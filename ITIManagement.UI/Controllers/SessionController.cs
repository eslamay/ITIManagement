
using ITIManagement.BLL.Services;
using ITIManagement.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace ITIManagement.UI.Controllers

{
    
   

   
    
        public class SessionController : Controller
        {
            private readonly ISessionService _sessionService;

            public SessionController(ISessionService sessionService)
            {
                _sessionService = sessionService;
            }


        public IActionResult Index(string search = "", int page = 1, int pageSize = 10)
        {
            
            var allSessions = _sessionService.GetAll(search, 1, int.MaxValue).ToList();

            
            var pagedSessions = _sessionService.GetAll(search, page, pageSize).ToList();

            var model = new ITIManagement.BLL.Pagination.PageResult<SessionVM>
            {
                Items = pagedSessions,
                Page = page,
                PageSize = pageSize,
                TotalCount = allSessions.Count
            };

            return View(model);
        }



        public IActionResult Details(int id)
            {
                var session = _sessionService.GetById(id);
                if (session == null)
                    return NotFound();

                return View(session);
            }

           
            public IActionResult Create()
            {
                return View();
            }

            
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(SessionVM sessionVm)
            {
                if (ModelState.IsValid)
                {
                    _sessionService.Add(sessionVm);
                    return RedirectToAction(nameof(Index));
                }
                return View(sessionVm);
            }

            
            public IActionResult Edit(int id)
            {
                var session = _sessionService.GetById(id);
                if (session == null)
                    return NotFound();

                return View(session);
            }

           
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(SessionVM sessionVm)
            {
                if (ModelState.IsValid)
                {
                    _sessionService.Update(sessionVm);
                    return RedirectToAction(nameof(Index));
                }
                return View(sessionVm);
            }

            
            public IActionResult Delete(int id)
            {
                var session = _sessionService.GetById(id);
                if (session == null)
                    return NotFound();

                return View(session);
            }

           
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
           
                _sessionService.Delete(id);
                return RedirectToAction(nameof(Index));
            
            
               

        }
        }
    }

















