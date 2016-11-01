using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remnants
{
    class BuildingRegistry
    {
        private static int lastID = 0;
        //for resolving building <-> ID / Name, and allows a backwards resolution of ID <-> name
        private static Dictionary<int, BuildingFactory> intToBuildingMap = new Dictionary<int, BuildingFactory>();
        private static Dictionary<String, BuildingFactory> stringToBuildingMap = new Dictionary<string, BuildingFactory>();
        private static Dictionary<BuildingFactory, int> buildingToIntMap = new Dictionary<BuildingFactory, int>();
        private static Dictionary<BuildingFactory, String> buildingToStringMap = new Dictionary<BuildingFactory, string>();
        //Registers a building factory to a name, and an interal ID for serialization
        public static int registerBuilding(BuildingFactory factory, String name)
        {
            intToBuildingMap[lastID] = factory;
            stringToBuildingMap[name] = factory;
            buildingToIntMap[factory] = lastID;
            buildingToStringMap[factory] = name;
            lastID++;
            return lastID - 1;
        }
        //the typical method for creating buildings, from the UI handler
        public static Building createBuildingFromName(String name)
        {
            if(stringToBuildingMap.ContainsKey(name))
            {
                Building ret = stringToBuildingMap[name].createBuilding();
                ret.ID = getIDFromName(name);
                ret.name = name;
                return ret;
            }
            return null;
        }
        public static Building createBuildingFromID(int id)
        {
            if(intToBuildingMap.ContainsKey(id))
            {
                Building ret = intToBuildingMap[id].createBuilding();
                ret.ID = id;
                ret.name = getNameFromID(id);
            }
            return null;
        }
        //gets the ID of a building by name, -1 if no match was found
        public static int getIDFromName(String name)
        {
            if(stringToBuildingMap.ContainsKey(name))
            {
                return buildingToIntMap[stringToBuildingMap[name]];
            }
            return -1;
        }
        //gets the name of a building ID, if it exists, empty string otherwise
        public static String getNameFromID(int id)
        {
            if(intToBuildingMap.ContainsKey(id))
            {
                return buildingToStringMap[intToBuildingMap[id]];
            }
            return "";
        }
    }
    abstract class BuildingFactory
    {
        public abstract Building createBuilding();
    }
}
