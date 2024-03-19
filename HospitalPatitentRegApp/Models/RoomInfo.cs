using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalPatitentRegApp.Models
{
    public class RoomInfo
    {
        public bool GetRoomInfo(string place, out int placeRoom, out int placeBed)
        {
            bool room = int.TryParse(place.Split('_')[1], out placeRoom);
            bool bed = int.TryParse(place.Split('_')[2], out placeBed);
            if (room && bed)
            {
                return true;
            }
            return false;
        }
    }
}
