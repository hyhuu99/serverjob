using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.GeneralModel.ResumesModel
{
    public class ExpInfo
    {
        public Dropdown Qualification { get; set; }
        public string School { get; set; }
        public string Achievements { get; set; }
        public string Position { get; set; }
        public string PreviousCompany { get; set; }
        public Dropdown YearOfWork { get; set; }
        public Dropdown[] LevelLanguages { get; set; }
        public Dropdown[] Languages { get; set; }
        public string Description { get; set; }
        public string References { get; set; }
        public string Skills { get; set; }
    }
}
