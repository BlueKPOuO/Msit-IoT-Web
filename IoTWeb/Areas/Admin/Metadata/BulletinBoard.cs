﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IoTWeb.Models
{
    [MetadataType(typeof(BulletinBoardMetadata))]
    public partial class BulletinBoard
    {
        public int No { get; internal set; }
    }
}