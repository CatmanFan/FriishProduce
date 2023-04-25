using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce.Injectors
{
    public class Flash
    {
        public string SWF { get; set; }
        public bool saveData { get; set; }
        public string saveData_Text { get; set; }

        public void InsertSaveData()
        {
            // TO-DO:
            /* Create banner.tpl with single texture, and icons.tpl with 4 textures
             * Insert TPLs in 00000002.app/banner/[XX]
             * Replace 00000002.app/banner/[XX]/banner.ini:
             *    title_text = saveData_Text (URL encoded)
             *    icon_speed (slow/normal)
             * Replace 00000002.app/config/config.common.pcf:
             *    shared_object_capability = "on"
             *    vff_cache_size = 2048
             *    persistent_storage_total = 2048
             *    persistent_storage_per_movie = 1024
             */
        }
    }
}
