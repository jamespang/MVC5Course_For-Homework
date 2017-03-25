using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using ClosedXML.Excel;
using System.Reflection;
using System.IO;

namespace MVC5Course.Controllers
{
    public class 客戶資料Controller : Controller
    {
        客戶資料Repository 客戶資料repo = RepositoryHelper.Get客戶資料Repository();
        公司基本資料Repository 公司基本資料repo = RepositoryHelper.Get公司基本資料Repository();

        // GET: 客戶資料
        public ActionResult Index(string keyword, string 客戶分類)
        {
            ViewBag.keyword = keyword;
            ViewBag.客戶分類 = 客戶分類;

            var data = 客戶資料repo.All().AsQueryable();

            if (!String.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.客戶名稱.Contains(keyword));
            }

            if (!String.IsNullOrEmpty(客戶分類))
            {
                data = data.Where(p => p.客戶分類 == 客戶分類);
            }
            return View(data.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerInformation/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料repo.Add(客戶資料);
                客戶資料repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var db = 客戶資料repo.UnitOfWork.Context;
                db.Entry(客戶資料).State = EntityState.Modified;
                客戶資料repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = 客戶資料repo.Find(id);
            客戶資料repo.Delete(客戶資料);
            客戶資料repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            var db = 客戶資料repo.UnitOfWork.Context;
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult 取得客戶分類()
        {
            var allData = 公司基本資料repo.All();
            return Json(allData.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult 匯出Excel()
        {
            var allData = 客戶資料repo.All();
            var workbook = new XLWorkbook();
            DataTable dt = ConvertObjectsToDataTable(allData);
            var worksheet = workbook.Worksheets.Add(dt, "客戶資料");
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "客戶資料.xlsx");
        }

        public static DataTable ConvertObjectsToDataTable(IEnumerable<object> objects)
        {
            DataTable dt = null;

            if (objects != null && objects.Count() > 0)
            {
                Type type = objects.First().GetType();
                dt = new DataTable(type.Name);

                foreach (PropertyInfo property in type.GetProperties())
                {
                    dt.Columns.Add(new DataColumn(property.Name));
                }

                foreach (FieldInfo field in type.GetFields())
                {
                    dt.Columns.Add(new DataColumn(field.Name));
                }

                foreach (object obj in objects)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn column in dt.Columns)
                    {
                        PropertyInfo propertyInfo = type.GetProperty(column.ColumnName);
                        if (propertyInfo != null)
                        {
                            dr[column.ColumnName] = propertyInfo.GetValue(obj, null);
                        }

                        FieldInfo fieldInfo = type.GetField(column.ColumnName);
                        if (fieldInfo != null)
                        {
                            dr[column.ColumnName] = fieldInfo.GetValue(obj);
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
    }
}
