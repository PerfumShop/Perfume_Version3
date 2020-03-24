using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace S3.Train.WebPerFume.Areas.Admin.Models
{
    public class ProductIndexViewModel : IndexViewModelBase
    {
        public IPagedList<Product> Paged { get; set; }
        public List<ProductViewModel> Items { get; set; }
    }

    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public List<VolumeCheckBoxModel> Volumes { get; set; }
        public List<ProVarationViewModel> ProVa { get; set; }

        [Display(Name = "Vendor")]
        public Guid Vendor_Id { get; set; }

        [Display(Name = "Brand")]
        public Guid Brand_Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Create Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Create Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? UpdateDate { get; set; }

        [Display(Name = "Image")]
        [Required]
        public string ImagePath { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Required]
        public List<SelectListItem> DropDownBrand { get; set; }

        [Required]
        public List<SelectListItem> DropDownVendor { get; set; }

        [Required]
        public List<Guid> SelecteCategories { get; set; }

        public List<SelectListItem> DropDowncategories { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual Brand Brand { get; set; }
        public DateTime CreatedDate { get; internal set; }
    }

    public class VolumeCheckBoxModel
    {
        public int id { get; set; }
        public string Volume { get; set; }
        public bool Checked { get; set; }
    }
}