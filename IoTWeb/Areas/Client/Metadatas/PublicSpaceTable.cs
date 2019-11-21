using IoTWeb.Areas.Client.Metadatas;
using IoTWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IoTWeb.Models
{
    [MetadataType(typeof(PublicSpaceMetadata))]
    public partial class PublicSpaceTable
    {
    }
}