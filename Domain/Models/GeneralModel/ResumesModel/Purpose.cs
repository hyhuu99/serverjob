using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.GeneralModel.ResumesModel
{
    public class Purpose
    {
        public City[] PlaceOfWorks { get; set; }
        public Dropdown[] Categorys { get; set; }
        public Dropdown LevelOfWork { get; set; }
        public Dropdown TypeOfWork { get; set; }
        public string Salary { get; set; }
        public string Ambition { get; set; }
        public bool CanChangePlace { get; set; }
    }
}
