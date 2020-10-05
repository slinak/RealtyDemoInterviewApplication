using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RealtyDemo.Models
{
    public class RealtyListingModel
    {
        [Key]
        public int MLSID {get;set; }


        [Display(Name = "Address Line 1")]
        public string StreetAddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string StreetAddressLine2 { get; set; }

        [Display(Name = "City")]
        public string StreetAddressCity { get; set; }

        [Display(Name = "State")]
        public string StreetAddressState { get; set; }

        [Display(Name = "Zip Code")]
        public string StreetAddressZipCode { get; set; }

        [Display(Name = "Neighborhood")]
        public string StreetAddressNeighborhood { get; set; }


        [Display(Name = "Sale Price ($)")]
        public double SalePrice { get; set; }

        [Display(Name = "Date Listed")]
        public DateTime InitialListingDate { get; set; }

        [Display(Name = "Bedrooms")]
        public int BedroomCount { get; set; }

        [Display(Name = "Photos")]
        public string Pictures { get; set; }

        [Display(Name = "Bathrooms")]
        public int BathroomCount { get; set; }


        [Display(Name = "Garage Size")]
        public double GarageSizeSquareFeet { get; set; }

        [Display(Name = "Square Feet")]
        public double HouseSizeSquareFeet { get; set; }

        [Display(Name = "Lot Size")]
        public double LotSizeSquareFeet { get; set; }


        [Display(Name = "Description")]
        public string Description { get; set; }

        public RealtyListingModel()
        {
            InitialListingDate = DateTime.Now;
        }
    }

    public static class RealtyListingHelper
    {
        public static IEnumerable<T> SearchAllAttributes<T>(this IEnumerable<T> obj, string searchKey)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance);

            if (properties == null)
                throw new ArgumentException("{typeof(T).Name}' does not implement a public get property named '{key}.");

            return obj.Where(d => properties.Any(p => (p.GetValue(d) ?? "").ToString().Contains(searchKey)) || properties.Any(p => (p.GetValue(d) ?? "").ToString().Equals(searchKey, StringComparison.InvariantCultureIgnoreCase))).ToList();
        }

        public static IEnumerable<T> SearchSpecificAttribute<T>(this IEnumerable<T> obj, string searchColumn, string searchKey)
        {
            var prop = typeof(T).GetProperty(searchColumn);

            if (prop == null)
                throw new ArgumentException("{typeof(T).Name}' does not implement a public get property named '" + searchColumn + "'.");

            return obj.Where(d => prop.GetValue(d).ToString().Contains(searchKey) || prop.GetValue(d).ToString().Equals(searchKey, StringComparison.InvariantCultureIgnoreCase));

        }
    }
}