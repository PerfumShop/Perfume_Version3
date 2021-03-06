﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Parent Categories")]
        public Guid? ParentId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? UpdatedDate { get; set; }

        public bool IsActive { get; set; }

        public IList<ProductViewModel> ProductCategoriesModels { get; set; }

        public List<Guid> SelectedProducts { get; set; }

        [Display(Name = "Product")]
        public List<SelectListItem> DropDownProduct { get; set; }
        [Display(Name = "Category Parent")]
        public List<SelectListItem> DropDownCategory { get; set; }

        [Required]
        public string Name { get; set; }
        public string Summary { get; set; }

    }
}