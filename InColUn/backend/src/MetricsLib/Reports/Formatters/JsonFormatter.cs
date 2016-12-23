using System;
using miniMetrics.Metric;
using miniMetrics.Utils;

namespace miniMetrics.Reports.Formatters
{
    /*
     * {
     *  report:"reportName",
     *  date:"date",
     *  gouges:[
     *   { name: gouge1, value:10},
     *   { name: gouge2, value:11}   
     *  ],
     *  timers:
     *  [
     *      { 
     *        name: timer1,
     *        values: [
     *           { start:"10/1/1", end:"10/1/1", duration:1001},
     *           { start:"10/1/1", end:"10/1/1", duration:1001},
     *           { start:"10/1/1", end:"10/1/1", duration:1001}
     *        ]
     *      }
     *  ],
     *  counters:[
     *      {name:"counter1", value:10},
     *      {name:"counter2", value:11},
     *      {name:"counter3", value:12}
     *  ],
     *  meters:[
     *      {name:"meter1", rate:"10.1", interval:"1s"},
     *      {name:"meter2", rate:"10.2", interval:"1m"},
     *      {name:"meter3", rate:"10.3", interval:"10s"},
     *  ],
     *  snapshorts:[
     *      { 
     *          name:"name1", 
     *          values:[
     *              {value:10, date:"1/1/1"},
     *              {value:11, date:"1/1/2"}
     *          ]
     *  ]
     * }
     */
    class JsonFormatter : BaseFormatter
    {
        public JsonFormatter(Action<string> callback):base(callback)
        {
        }

        public override void StartContext(string contextName) { }
        public override void StartReport(string reportName) { }
        public override void StartMetricGroup(string groupName) { }
        public override void EndMetricGroup(string groupName) { }
        public override void EndReport(string reportName) { }
        public override void EndContext(string contextName) { }

        public override void ReportGauge(string name, double value, Unit unit)
        {

        }
        public override void ReportCounter(string name, Counter value, Unit unit)
        {

        }
        public override void ReportMeter(string name, Meter value, Unit unit, TimeUnit rateUnit)
        {

        }
        public override void ReportTimer(string name, TimeInterval value, TimeUnit resolution)
        {

        }
    }
}
