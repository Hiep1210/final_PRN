using System;
using System.Collections.Generic;

namespace Final_Project_PRN.Models
{
    public partial class RoomBuilding
    {
        public string BuildingType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int NumberOfRooms { get; set; }
    }
}
