﻿using Commons.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CollectService.Param
{
  public  class CollectParam : QueryParam
    {
        public long pid { get; set; }

        public string type { get; set; }
    }
}
