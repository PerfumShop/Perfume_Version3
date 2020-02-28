﻿using S3.Train.WebPerFume.Areas.Admin.Models;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.CommonFunction
{
    public static class DropDownListDomain
    {
        /// <summary>
        /// List checkbox volume
        /// </summary>
        /// <returns>Volume Check Box List</returns>
        public static List<VolumeCheckBoxModel> GetVolumeCheckBoxes()
        {
            var list = new List<VolumeCheckBoxModel>
            {
                new VolumeCheckBoxModel{id = 1, Volume = "25ml"},
                new VolumeCheckBoxModel{id = 1, Volume = "50ml"},
                new VolumeCheckBoxModel{id = 1, Volume = "100ml"},
                new VolumeCheckBoxModel{id = 1, Volume = "150ml"},
                new VolumeCheckBoxModel{id = 1, Volume = "200ml"}
            };

            return list;
        }

        /// <summary>
        /// Drop Down List Brand
        /// </summary>
        /// <param name="brands"></param>
        /// <returns>Brand Select List Item</returns>
        public static List<SelectListItem> DropDownList_Brand(IList<Brand> brands)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in brands.OrderBy(p => p.Name).ToList())
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return items;
        }

        /// <summary>
        /// Drop Down List Vendor
        /// </summary>
        /// <returns>Vendor Select List Item</returns>
        public static List<SelectListItem> DropDownList_Vendor(IList<Vendor> vendors)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in vendors.OrderBy(p => p.Name).ToList())
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return items;
        }

        /// <summary>
        /// Drop Down List Product
        /// </summary>
        /// <param name="products"></param>
        /// <returns>Product Select List Item </returns>
        public static List<SelectListItem> DropDownList_Product(IList<Product> products)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in products.OrderBy(p => p.Name).ToList())
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return items;
        }

        /// <summary>
        /// Drop Down List Volume
        /// </summary>
        /// <returns>Volume Select List Item</returns>
        public static List<SelectListItem> DropDownList_Volume()
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text ="25ml" , Value = "25ml"},
                new SelectListItem{Text ="50ml" , Value = "50ml"},
                new SelectListItem{Text ="100ml" , Value = "100ml"},
                new SelectListItem{Text ="150ml" , Value = "150ml"},
                new SelectListItem{Text ="200ml" , Value = "200ml"}
            };
            
            return items;
        }
    }

}