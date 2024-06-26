﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace LetsSale.Models
{
    public class AddCarViewModel
    {
        public Guid CMainID { get; set; }
        public string CarName { get; set; }
        public string CarBrand { get; set; }
        public int CarPrice { get; set; }
        public string CarPlateNumber { get; set; }
        public int CarYear { get; set; }
        public string CarColor { get; set; }
        public int CarHP { get; set; }
        public int CarTorque { get; set; }
        public int CarMaxFuelTankCapacity { get; set; }
        public int CarMaxSpeed { get; set; }
        public string CarTransmission { get; set; }
        public int CarCargoVolume { get; set; }
        public string CarMainPic { get; set; }
        public int CarCategoryID { get; set; }
        public Guid EmployeeID { get; set; }
        public SelectList CarCategories { get; set; }
    }
}
