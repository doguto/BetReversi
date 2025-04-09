using System.Threading.Tasks;

namespace Project.Common.Model
{
    internal static class Interval
    {
        internal static async Task Deray(int derayTime)
        {
            await Task.Delay(1000);
        }
    }
}