using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using TDK.Core.Logic.DAL;

namespace TDK.Core.Logic.BLL
{
    public partial class BllManager
    {
        /// <summary>
        /// 生成Block之间的连线关系，Signal的集合，并存储数据
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public IDictionary<string, Cld_Signal> GenerateSignalLines(Prj_Sheet sheet)
        {
            // 测点名与起始引脚的对应关系
            IDictionary<string, Cld_FCOutput> pointNameToStartPin = new Dictionary<string, Cld_FCOutput>();
            // 
            IDictionary<string, Cld_Signal> pointNameToSignal = new Dictionary<string, Cld_Signal>();

            // 匹配PointName格式为"%-%-%-%"的项
            Regex regPointName = new Regex(@"^(\d+)((-(\d+)){3})$");

            foreach (Cld_FCBlock block in sheet.Cld_FCBlock_List)
            {
                foreach (Cld_FCOutput output in sheet.Cld_FCOutput_List)
                {
                    // 将当前页面所有 Output 与点名的对应关系存储在 pointNameToStartPin 字典中
                    // 在该字典中，通过 PointName 就可以找到对应的 Output
                    if (!pointNameToStartPin.ContainsKey(output.PointName))
                    {
                        pointNameToStartPin.Add(output.PointName, output);
                    }
                }
            }

            foreach (Cld_FCBlock block in sheet.Cld_FCBlock_List)
            {
                foreach (Cld_FCInput input in block.Cld_FCInput_List)
                {
                    if (input.PointName == null || !regPointName.IsMatch(input.PointName) || !input.Visible
                        || !pointNameToStartPin.ContainsKey(input.PointName))
                    {
                        // 该管脚未连接，或不显示信号线，或连接的Pin不在当前Sheet中，继续扫描下一个 Input。
                        continue;
                    }

                    Cld_FCOutput startPin = pointNameToStartPin[input.PointName] as Cld_FCOutput;
                    string signalData;

                    if (startPin.Point != null || input.Point != null)
                    {
                        signalData = GenerateSignalData(startPin, input);
                    }
                    else
                    {
                        throw new Exception("管脚坐标未计算！");
                    }

                    Cld_Signal signalTemp;

                    if (pointNameToSignal.ContainsKey(input.PointName))
                    {
                        signalTemp = pointNameToSignal[input.PointName];
                        signalTemp.Data += signalData;
                        pointNameToSignal[input.PointName] = signalTemp;

                        //manager.Update(signal_temp);
                    }
                    else
                    {
                        signalTemp = new Cld_Signal();
                        signalTemp.Prj_Controller = sheet.Prj_Controller;
                        signalTemp.Prj_Document = sheet.Prj_Document;
                        signalTemp.Prj_Sheet = sheet;
                        signalTemp.Name = startPin.PointName;
                        signalTemp.Data = signalData;

                        signalTemp.SignalType = get_pin_dataType(startPin.Cld_FCBlock.FunctionName, startPin.PinName);

                        pointNameToSignal.Add(input.PointName, signalTemp);

                        //this.manager.Save(signal_temp);
                    }
                }
            }
            //this.manager.Flush();

            return pointNameToSignal;
        }

        /// <summary>
        /// 生成一条连接两个Pin的线
        /// </summary>
        /// <param name="startPin">起始Pin</param>
        /// <param name="endPin">结束Pin</param>
        /// <returns>表示一条线的字符串</returns>
        public string GenerateSignalData(Cld_FCOutput startPin, Cld_FCInput endPin)
        {
            Cld_FCBlock startBlock = startPin.Cld_FCBlock;
            Cld_FCBlock endBlock = endPin.Cld_FCBlock;
            IList startInputList = startBlock.Cld_FCInput_List;
            IList startOutputList = startBlock.Cld_FCOutput_List;
            IList endInputList = endBlock.Cld_FCInput_List;
            IList endOutputList = endBlock.Cld_FCOutput_List;
            PointF startPoint = new PointF(startBlock.X + startPin.X, startBlock.Y + startPin.Y);
            PointF endPoint = new PointF(endBlock.X + endPin.X, endBlock.Y + endPin.Y);

            StringBuilder signalDatails = new StringBuilder();

            const float spacing = 8f;       // 相邻两个输入引脚的延伸长度差值
            const float pinLength = 16f;    // 引脚的最小延伸长度


            int startPinIndex;
            int endPinIndex;

            // 添加开始点
            signalDatails.Append(startPoint.X + "_" + startPoint.Y + "{"
                + startBlock.AlgName + "." + startPin.PinName + "},");

            if (startPoint.Y != endPoint.Y)
            {
                // 起止点的垂直坐标不在同一直线上，需要增加折点

                if (startPoint.Y < endPoint.Y)
                {
                    startPinIndex = startPin.PinIndex;
                    endPinIndex = endPin.PinIndex;
                }
                else
                {
                    startPinIndex = startOutputList.Count - startPin.PinIndex - 1;
                    endPinIndex = endInputList.Count - endPin.PinIndex - 1;
                }

                PointF point = new PointF();
                point.Y = startPoint.Y;
                float firstX = startPoint.X + pinLength + spacing * startPinIndex;  // 第一个折点的 X 坐标
                float lastX = endPoint.X - pinLength - spacing * endPinIndex;       // 最后一个折点的 X 坐标

                if (firstX < lastX)
                {
                    point.X = lastX;
                }
                else
                {
                    if (startPoint.X < endPoint.X)
                    {
                        point.X = (endPoint.X + startPoint.X) / 2;
                    }
                    else
                    {
                        point.X = firstX;
                        signalDatails.Append(point.X + "_" + point.Y + ",");

                        point.Y = Math.Abs(endPoint.Y + startPoint.Y) / 2;
                        signalDatails.Append(point.X + "_" + point.Y + ",");

                        point.X = lastX;
                    }
                }

                signalDatails.Append(point.X + "_" + point.Y + "," + point.X + "_" + endPoint.Y + ",");
            }
            else if (startPoint.X > endPoint.X)
            {
                // 起止点垂直坐标相同，但起点比终点水平坐标值大

                float upHeight = (float)(endPoint.Y - endBlock.Y);
                float downHeight = (float)(endBlock.Y + endBlock.Symbol.height - endPoint.Y);

                float FirstX;
                float lastX;
                float signalY;

                if (upHeight < downHeight)
                {
                    // 从Block上面折回
                    FirstX = startPoint.X + pinLength + spacing * startPin.PinIndex;
                    signalY = (float)(endBlock.Y - pinLength - spacing * endPin.PinIndex);
                    lastX = endPoint.X - pinLength - spacing * endPin.PinIndex;
                }
                else
                {

                    FirstX = startPoint.X + pinLength + spacing
                        * (startOutputList.Count - startPin.PinIndex - 1);
                    signalY = (float)(endBlock.Y + endBlock.Symbol.height + pinLength + spacing
                        * (endInputList.Count - endPin.PinIndex - 1));
                    lastX = endPoint.X - pinLength - spacing
                        * (endInputList.Count - endPin.PinIndex - 1);
                }

                signalDatails.Append(FirstX + "_" + startPoint.Y + "," + FirstX + "_" + signalY + ","
                    + lastX + "_" + signalY + "," + lastX + "_" + endPoint.Y + ",");
            }

            // 添加结束点
            signalDatails.Append(endPoint.X + "_" + endPoint.Y + "{"
                + endBlock.AlgName + "." + endPin.PinName + "};");

            return signalDatails.ToString();
        }
    }
}
