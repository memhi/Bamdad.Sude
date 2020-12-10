using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Account;
using Sude.Mvc.UI.ApiManagement;
using Microsoft.AspNetCore.Mvc.Rendering;
 

namespace Sude.Mvc.UI.Admin.Controllers.Customer
{
    public class CustomerController : BaseAdminController
    {

        [HttpGet]
        public async Task<ActionResult> AddOrderCustomer()
        {



            return PartialView("AddOrderCustomer");
        }

        [HttpPost]
        public async Task<ActionResult> AddOrderCustomer(CustomerNewDtoModel request)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = message
                });
            }

            string CurrentWorkId = HttpContext.Session.GetString("CurrentWorkId");
            request.WorkId = CurrentWorkId;
            ResultSetDto<CustomerNewDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<CustomerNewDtoModel>>(ApiAddress.Customer.AddCustomer, request);


            return Json(result);

        }


        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // [Authorize]
        public async Task<ActionResult> GetCurrentCustomers()
        {
            //System.Threading.Thread.Sleep(1000);
            string CurrentWorkId = HttpContext.Session.GetString("CurrentWorkId");

            ResultSetDto<IEnumerable<CustomerDetailDtoModel>> Customerslist = await Api.GetHandler
    .GetApiAsync<ResultSetDto<IEnumerable<CustomerDetailDtoModel>>>(ApiAddress.Customer.GetCustomersByWorkId + CurrentWorkId);

            SelectList selectLists = new SelectList(Customerslist.Data as ICollection<CustomerDetailDtoModel>, "CustomerId", "Title");
            // ViewData["Customers"] = selectLists;

            return Json(selectLists);
        }


        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
