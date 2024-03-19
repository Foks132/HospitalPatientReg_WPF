using HospitalPatitentRegApp.Entities;
using HospitalPatitentRegApp.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HospitalPatitentRegApp.Models
{
    public class DbRoomBedRows
    {
        List<FrameworkElement> bedElements = new List<FrameworkElement>();
        List<D_HospitalBedFund> dbContext = App.GetContext().D_HospitalBedFund.ToList();

        public DbRoomBedRows(HospitalRoomsPage page)
        {
            foreach (FrameworkElement room in page.Rooms.Children)
            {
                if (room is Grid && room.Name.Contains("Room_"))
                {
                    int roomId = int.Parse(room.Name.Split('_')[1]);

                    foreach (FrameworkElement bed in ((Grid)room).Children)
                    {
                        if (bed is Button && bed.Name.Contains(roomId.ToString()))
                        {
                            bedElements.Add(bed);
                            int bedId = int.Parse(bed.Name.Split('_')[2]);
                            if (dbContext.FirstOrDefault(x => x.Bed == bedId &&
                                x.Room == roomId) == null)
                            {
                                App.GetContext().D_HospitalBedFund.Add(
                                new D_HospitalBedFund()
                                {
                                    Room = roomId,
                                    Bed = bedId
                                });
                            }
                        }
                    }
                }
            }
            App.GetContext().SaveChanges();
        }

        public List<FrameworkElement> GetBedElements()
        {
            return bedElements;
        }
    }
}
