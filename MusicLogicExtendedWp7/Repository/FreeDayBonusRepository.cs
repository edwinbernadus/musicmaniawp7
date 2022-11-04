using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSPToolkit.Utilities;
using Model;
using Newtonsoft.Json;
using SharedLogic;

namespace MusicLogicExtendedWp7.Repository
{
    public class FreeDayBonusRepository
    {
        //http://www.timeapi.org/utc/now
        public async Task<bool> AddGetOne(string datePeriode)
        {
            try
            {
                var currentData = await Get(datePeriode);
                if (currentData.FreeBonusNotUse < 2)
                {
                    currentData.FreeBonusNotUse++;
                    await Set(datePeriode, currentData);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task UseForDownload(string datePeriode)
        {
            try
            {
                var currentData = await Get(datePeriode);
                currentData.FreeBonusAlreadyUsedForDownload++;
                await Set(datePeriode, currentData);
            }
            catch (Exception exception)
            {
            }
        }
     
        private static Dictionary<string, FreeBonus> TotalPoint = new Dictionary<string, FreeBonus>();

        private async Task Set(string periode, FreeBonus freeBonus)
        {
            TotalPoint.Remove(periode);
            TotalPoint.Add(periode,freeBonus);
            
            try
            {
                var t = JsonConvert.SerializeObject(freeBonus);
                await FileHelperAsync.TextToFile(periode, t);
            }
            catch (Exception exception)
            {

            }
            
        }

        public async Task<FreeBonus> Get(string periode)
        {
            FreeBonus output = new FreeBonus()
                                   {
                                       FreeBonusAlreadyUsedForDownload = 0,
                                       FreeBonusNotUse = 0,
                                       Period = periode
                                   };

            FreeBonus t;
            var hasDataInMemory = TotalPoint.TryGetValue(periode, out t);
            if (!hasDataInMemory)
            {
                try
                {
                    var item = await FileHelperAsync.ReadFromFile(periode);
                    t  = JsonConvert.DeserializeObject<FreeBonus>(item);
                    if (t != null)
                    {
                        output = t;
                    }
                }
                catch (Exception exception)
                {
                    
                }
            }
            else
            {
                output = t;
            }

            return output;
        }

    }
}