using PersonalWebsite.Shared.Enums;
using System;
using System.Linq;

namespace PersonalWebsite.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var genericEnumType = value.GetType();
            var memberInfo = genericEnumType.GetMember(value.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                    return ((System.ComponentModel.DescriptionAttribute)attributes.ElementAt(0)).Description;
            }

            return value.ToString();
        }

        public static bool IsGenerallyNorth(this NeighbourDirection dir)
        {
            return dir switch
            {
                NeighbourDirection.North => true,
                NeighbourDirection.NorthEast => true,
                NeighbourDirection.NorthWest => true,
                _ => false
            };
        }

        public static bool IsGenerallyEast(this NeighbourDirection dir)
        {
            return dir switch
            {
                NeighbourDirection.East => true,
                NeighbourDirection.NorthEast => true,
                NeighbourDirection.SouthEast => true,
                _ => false
            };
        }

        public static bool IsGenerallySouth(this NeighbourDirection dir)
        {
            return dir switch
            {
                NeighbourDirection.South => true,
                NeighbourDirection.SouthEast => true,
                NeighbourDirection.SouthWest => true,
                _ => false
            };
        }

        public static bool IsGenerallyWest(this NeighbourDirection dir)
        {
            return dir switch
            {
                NeighbourDirection.West => true,
                NeighbourDirection.NorthWest => true,
                NeighbourDirection.SouthWest => true,
                _ => false
            };
        }
    }
}
