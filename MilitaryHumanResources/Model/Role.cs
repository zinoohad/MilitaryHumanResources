﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryHumanResources.Model
{
    public class Role
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public override bool Equals(object obj)
        {
            var role = obj as Role;
            return role != null &&
                   ID == role.ID;
        }

        public override int GetHashCode()
        {
            var hashCode = 1831404553;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            return hashCode;
        }
    }
}
