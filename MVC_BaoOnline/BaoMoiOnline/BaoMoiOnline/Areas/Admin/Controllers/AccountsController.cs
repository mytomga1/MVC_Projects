using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaoMoiOnline.Models;
using BaoOnline.Extensions;
using PagedList.Core;
using BaoOnline.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BaoMoiOnline.Areas.Admin.Models;

namespace BaoMoiOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {
        private readonly BaoOnlineContext _context;

        public AccountsController(BaoOnlineContext context)
        {
            _context = context;
        }

        // GET: Admin/Accounts

        public IActionResult Index(int? page)
        {

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;// nếu page bằng null thì gắng giá trị bằng 1, còn ko thì giá trị = value
            var pageSize = Utilities.PAGE_SIZE;// PAGE_SIZE ở thư mục Utilities dc gán giá trị sẳn là 20
            var lsAccounts = _context.Accounts.Include(a => a.Role)
                .OrderByDescending(x => x.CreatedDate);


            // gọi sử dụng thư viện PagedList
            PagedList<Account> models = new PagedList<Account>(lsAccounts, pageNumber, pageSize);

            //ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/Login
        [HttpGet]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "Login")]
        public IActionResult Login(string returnUrl = null)
        {

            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID != null) return RedirectToAction("Index", "Home", new { Areas = "Admin" });
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("dang-nhap.html", Name = "Login")]
        //public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        //{
        //    try {

        //        if (ModelState.IsValid) {

        //            Account kh = _context.Accounts
        //                .Include(p => p.Role)
        //                .SingleOrDefault(p => p.Email.ToLower() == model.Email.ToLower().Trim());
        //            if (kh != null) {

        //                ViewBag.Error = "Thông tin đăng nhập chưa chính xác";
        //                return View(model);
        //            }
        //            string pass = (model.Password.Trim() + kh.Salt.Trim()).ToMD5();
        //            if (kh.Password.Trim() != pass)
        //            {

        //                ViewBag.Error = "Thông tin đăng nhập chưa chính xác";
        //                return View(model);
        //            }
        //            // Đăng Nhập Thành Công

        //            //Ghi nhận thời gian đăng nhập
        //            kh.LastLogin = DateTime.Now;
        //            _context.Update(kh);
        //            await _context.SaveChangesAsync();

        //            var taikhoanID = HttpContext.Session.GetString("AccountId");
        //            //Identity
        //            //Lưu Session MaKh
        //            HttpContext.Session.SetString("AccountId", kh.AccountId.ToString());

        //            //Identity
        //            var userClain = new List<Claim>
        //        }
        //    } catch { }
        //}

        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/Accounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,FullName,Email,Phone,Password,Salt,Active,CreatedDate,RoleId,LastLogin")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,FullName,Email,Phone,Password,Salt,Active,CreatedDate,RoleId,LastLogin")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}