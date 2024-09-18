using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext context;
        public AccountsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var users = context.Users.ToList();
            return View(users);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return RedirectToAction(nameof(SignIn));
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(User user)
        {
            var checkUser = context.Users.Where(u => u.Email == user.Email && u.Password == user.Password);
            if (checkUser.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        public IActionResult Update(Guid id)
        {
            var user = context.Users.Find(id);
            return View(user);
        }
        public IActionResult Edit(User user)
        {
            var prevUser = context.Users.Find(user.Id);
            prevUser.Name = user.Name;
            prevUser.Email = user.Email;
            if(user.Password is not null)
            {
                prevUser.Password = user.Password;
            }
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(Guid id)
        {
            var user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ActiveUsers()
        {
            var ActiveUsers = context.Users.Where(user => user.IsActived == true).ToList();
            return View(ActiveUsers);
        }
        public IActionResult ToUnActive(Guid id)
        {
            var user = context.Users.Find(id);
            user.IsActived = false;
            context.SaveChanges();
            return RedirectToAction(nameof(ActiveUsers));
        }
        public IActionResult UnActiveUsers()
        {
            var UnActiveUsers = context.Users.Where(user => user.IsActived == false).ToList();
            return View(UnActiveUsers);
        }
        public IActionResult ToActive(Guid id)
        {
            var user = context.Users.Find(id);
            user.IsActived = true;
            context.SaveChanges();
            return RedirectToAction(nameof(UnActiveUsers));
        }
    }

}
