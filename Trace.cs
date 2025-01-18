using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using System.Runtime.InteropServices;

namespace MBKLibrary;

public class Trace
{
    public static void Ray()
    {
        Vector start = new Vector(0, 0, 0);
        Vector end = new Vector(100, 50, 25);

        IntPtr ray = NativeAPI.CreateRay1(0, start.Handle, end.Handle);
    }

    public void PerformSimpleRayTrace(Vector start, Vector end, int ignoredEntityIndex)
    {
        // Step 1: Create a ray using the handles of Vector objects
        IntPtr ray = NativeAPI.CreateRay1(0, start.Handle, end.Handle);

        // Step 2: Create a trace result object
        IntPtr traceResult = NativeAPI.NewTraceResult();

        // Step 3: Create a simple trace filter
        IntPtr traceFilter = NativeAPI.NewSimpleTraceFilter(indexToIgnore: ignoredEntityIndex);

        // Step 4: Perform the trace
        NativeAPI.TraceRay(ray, traceResult, traceFilter, flags: 0);

        // Step 5: Process the result
        /*var result = Marshal.PtrToStructure<TraceResultStruct>(traceResult);
        Console.WriteLine($"Hit entity: {result.HitEntity}");
        Console.WriteLine($"Hit position: {result.HitPosition}");*/
    }

}
