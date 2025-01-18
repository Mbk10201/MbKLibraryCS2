using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;

namespace MBKLibrary;

public class Utility
{
    public static Random Random = new Random();

    /// <summary>
    /// Print message into the chat of everyone.
    /// </summary>
    /// <param name="message">the message</param>
    public static void PrintToChat(string message)
    {
        VirtualFunctions.ClientPrintAll(HudDestination.Chat, message, 0, 0, 0, 0);
    }

    public static void PrintToConsole(string s) => NativeAPI.PrintToServerConsole($"{s}\n\0");

    public static void DisableTeamSelect(bool value)
    {
        var cvar = ConVar.Find("sv_disable_teamselect_menu");
        if (cvar != null)
            cvar.SetValue(true);
        else
            Console.WriteLine("Convar sv_disable_teamselect_menu not found");
    }

    public static void SetHudColorByTeam(CCSPlayerController player)
    {
        var colorCode = 2;
        colorCode = (player.Team == CsTeam.CounterTerrorist ? 4 : 3);

        player.ExecuteClientCommandFromServer($"cl_hud_color {colorCode}");
    }

    public static void KillTimer(CounterStrikeSharp.API.Modules.Timers.Timer timer)
    {
        timer?.Kill();
        timer = null;
    }

    /// <summary>
    /// Get all players in Terrorist team.
    /// </summary>
    public static IEnumerable<CCSPlayerController> GetAllT() => Utilities.GetPlayers().Where(x => x.Team == CsTeam.Terrorist);

    /// <summary>
    /// Get all players in Counter-Terrorist team.
    /// </summary>
    public static IEnumerable<CCSPlayerController> GetAllCT() => Utilities.GetPlayers().Where(x => x.Team == CsTeam.CounterTerrorist);
}
