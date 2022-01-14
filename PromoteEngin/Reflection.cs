using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace caTest
{
    public class Reflection
    {
        public void run()
        {
            //定義等一下要調用的物件名稱
            //請注意，這邊並沒有使用「早期繫結」的方式，把物件名稱寫死在程式碼之中
            string cTargetClassName = "caTest.Promotion";
            string cTargetProperty = "item";
            string cTargetMethodName = "item";

            //get assembly info
            Assembly info = System.Type.GetType(cTargetClassName).Assembly;
            Console.WriteLine(info);

            //Object[] constructParms = new object[] {"abcde",11,"F",10};
            System.Object oTemp = System.Activator.CreateInstance(System.Type.GetType(cTargetClassName));
            System.Type oTypeBase = System.Type.GetType(cTargetClassName);

            //設定與展示物件的名稱屬性
            System.Reflection.PropertyInfo oProperty = oTypeBase.GetProperty(cTargetProperty);
            oProperty.SetValue(oTemp, "abca");
            oProperty = oTypeBase.GetProperty("OriginPrice");
            oProperty.SetValue(oTemp, Convert.ToDecimal(11));
            oProperty = oTypeBase.GetProperty("PromotionType");
            oProperty.SetValue(oTemp, "F");
            oProperty = oTypeBase.GetProperty("Param");
            oProperty.SetValue(oTemp, Convert.ToDecimal(22));
            //System.Console.Write("Property: {0}", oProperty.GetValue(oTemp));

            System.Reflection.PropertyInfo[] properties = oTypeBase.GetProperties();

            foreach (var pi in properties)
            {

                System.Console.WriteLine($"{pi.Name}: {pi.GetValue(oTemp)} + {pi.PropertyType}");
            }

            ////依使用者選擇的「字串」，動態轉換物件去調用介面成員
            //System.Object oEntity;
            //switch (iMachineType)
            //{
            //    case 2:
            //        oTypeBase = System.Type.GetType("Reflection.ICar");
            //        oEntity = (ICar)oTemp;
            //        break;
            //    default:
            //        oTypeBase = System.Type.GetType("Reflection.IBike");
            //        oEntity = (IBike)oTemp;
            //        break;
            //}
            //System.Reflection.MethodInfo oMethod = oTypeBase.GetMethod(cTargetMethodName);
            //System.Console.WriteLine("擁有{0}顆輪子。", oMethod.Invoke(oEntity, null));

            System.Console.Read();
        }
    }
}
