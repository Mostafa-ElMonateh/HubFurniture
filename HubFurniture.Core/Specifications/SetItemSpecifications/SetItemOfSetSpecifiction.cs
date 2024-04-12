using HubFurniture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Specifications.SetItemSpecifications
{
    public class SetItemOfSetSpecifiction : BaseSpecifications<SetItem>
    {
        public SetItemOfSetSpecifiction(int SetId)
            : base(si => si.CategorySetId == SetId)
        {

        }
    }
}
