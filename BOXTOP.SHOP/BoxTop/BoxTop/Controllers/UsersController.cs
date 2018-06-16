using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoxTop.Data;
using BoxTop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BoxTop.Functions;
using BoxTop.ProviderJWT;

namespace BoxTop.Controllers
{
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }



        //_____________________________________________________________________________________________________________________


        // GET: Users/Authorisation
        [Route("Authorisation")]
        public async Task<IActionResult> Authorisation()
        {
            return View();
        }

        [Route("Authorisation")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authoristation([Bind("Login, Password")] User realUser)
        {
            User userTruly = _context.Users.Where(s => s.Login == realUser.Login && s.Password == Hasher.GetHashString(realUser.Password)).FirstOrDefault<User>();
            if (userTruly == null)
            {
                return View("Error");
            }
            else
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JWTSecurityKey.Create("Test-secret-key-1234"))
                                .AddSubject(userTruly.Login)
                                .AddIssuer("Test.Security.Bearer")
                                .AddAudience("Test.Security.Bearer")
                                .AddClaim(userTruly.Role, userTruly.ID.ToString())
                                .AddExpiry(200)
                                .Build();
                userTruly.LastToken = token.Value;

                //Обновляем данные в БД по пользователю
                _context.Users.Update(userTruly);
                _context.SaveChanges();

                if (realUser != null)
                {
                    HttpContext.Session.SetString("Token", userTruly.LastToken);
                    HttpContext.Session.SetString("Login", userTruly.Login);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Error");
                }
            }
        }

        // GET: Users/LogOut
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            string user = HttpContext.Session.GetString("Login");
            user = user != null ? user : "";
            HttpContext.Session.SetString("Token", "");
            HttpContext.Session.SetString("Login", "");
            return RedirectToAction(nameof(Index), "Home");
        }

        //________________________________________________________________________________________________________________________

        // GET: Users
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        //ДОЛЖНО БЫТЬ ВИДНО САМОМУ 5 ПОЛЬЗОВАТЕЛЮ И АДМИНУ
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Login,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = "User";
                user.Password = Hasher.GetHashString(user.Password);
                _context.Add(user);
                await _context.SaveChangesAsync();

                //ЗАПИСЬ ТОКЕНА В СЕССИЮ, СОЗДАНИЕ ТОКЕНА


                return RedirectToAction("Index", "Home");
            }

            //
            return View(user);
        }

        // GET: Users/Edit/5
        //Должно быть доступно только для самого пользователя, админа
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        //Должно быть доступно только для самого пользователя, админа
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Login,Password,Role,LastToken")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.Password = Hasher.GetHashString(user.Password);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        //Должно быть доступно только для самого пользователя, админа
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        //Должно быть доступно только для самого пользователя, админа
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
