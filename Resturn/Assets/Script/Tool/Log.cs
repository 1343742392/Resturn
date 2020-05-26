using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class Log
{
    //out("name", name, "age", age);
    public static void Out(params object[] ts)
    {
        string res = "";
        var sw = true;
        foreach (object t in ts)
        {

            //vector3 精度问题
            if (t.GetType().Name.Equals("Vector3"))
            {
                Vector3 vec = (Vector3)t;

/*                var attrs = new Vector3().GetType().GetProperties();
                var tp = t.GetType();
                PropertyInfo mi = attrs[0];
                var x = mi.GetValue(t, new object[] { 0 });
                var y = mi.GetValue(t, new object[] { 1 });
                var z = mi.GetValue(t, new object[] { 2 });*/
                res += " Vector3  x=" + vec.x + "  y=" + vec.y + "  z=" + vec.z + " ";
                continue;
            }
            //特殊字符问题 0 替代空字符
            string st = t.ToString();
            var sb = new StringBuilder();

            for (int f = 0; f < st.Length; f++)
            {
                sb.Append(st[f] == '\0' ? "\\0" : st[f].ToString());
            }
            res += " " + sb.ToString() + " ";
            if (sw)
                res += ":";
            sw = !sw;
        }
        Debug.Log(res);
    }
}
