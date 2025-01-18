using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Utils;
using System.Drawing;

namespace MBKLibrary.Extensions;

public static partial class PlayerExtension
{
    public static Vector? EyeVector(this CCSPlayerController? player)
    {
        var pawn = player.Pawn.Value.As<CCSPlayerPawn>();

        if (pawn == null) return null;

        QAngle eyeAngle = pawn.EyeAngles;

        // convert angles to rad 
        double pitch = (Math.PI / 180) * eyeAngle.X;
        double yaw = (Math.PI / 180) * eyeAngle.Y;

        // get direction vector from angles
        Vector eyeVector = new Vector((float)(Math.Cos(yaw) * Math.Cos(pitch)), (float)(Math.Sin(yaw) * Math.Cos(pitch)), (float)(-Math.Sin(pitch)));

        return eyeVector;
    }

    public static void Disarm(this CCSPlayerController? player, params string[] excludes)
    {
        if (player == null) return;
        if (!player.Pawn.IsValid) return;
        if (player.Pawn.Value?.WeaponServices == null)
            return;

        //player.RemoveWeapons();

        foreach (var x in player.Pawn.Value.WeaponServices.MyWeapons)
        {
            if (x.Value == null)
                continue;

            if (excludes != null && excludes.Contains(x.Value.DesignerName))
                continue;

            player.RemoveItemByDesignerName(x.Value.DesignerName);
        }
    }

    public static void SetTag(this CCSPlayerController? player, string tag)
    {
        if (player == null) return;

        if (player.Clan == tag)
            return;

        player.Clan = tag;
        Utilities.SetStateChanged(player, "CCSPlayerController", "m_szClan");

        EventNextlevelChanged fakeEvent = new(false);
        fakeEvent.FireEventToClient(player);
    }

    public static CBaseEntity? GetAimTarget(this CCSPlayerController? player, bool onlyClients = true)
    {
        if (player == null) return null;

        var rules = Utilities.FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules").FirstOrDefault();

        if (rules == null) return null;

        if (onlyClients)
            return rules.GameRules?.GetClientAimTarget(player);
        else
            return rules.GameRules?.FindPickerEntity<CBaseEntity>(player);
    }

    public static float? GetDistanceTo(this CCSPlayerController? player, CBaseEntity entity) => player?.Pawn.Value?.GetDistanceTo(entity);

    public static void PlaySound(this CCSPlayerController? player, string sound)
    {
        if (player == null) return;
        
        if (string.IsNullOrEmpty(sound))
            return;

        Console.WriteLine($"Tryed to play sound: {sound}");

        player.ExecuteClientCommand($"play {sound}");
    }

    public static void SetHealth(this CCSPlayerController? player, int value)
    {
        var pawn = player?.Pawn.Value;
        if (pawn == null) return;

        pawn.Health = value;
        Utilities.SetStateChanged(pawn, "CBaseEntity", "m_iHealth");
    }

    public static void AddHealth(this CCSPlayerController? player, int value)
    {
        var pawn = player?.Pawn.Value;
        if (pawn == null) return;
        if (!player.PawnIsAlive) return;

        if (value > 100 || (player.Health + value > 100))
        {
            pawn.MaxHealth = value;
        }

        player.SetHealth(player.Health + value);
    }

    public static void SetRenderColor(this CCSPlayerController? player, Color color)
    {
        var pawn = player?.Pawn.Value;
        if (pawn == null) return;

        pawn.SetRenderColor(color);
    }

    public static bool HasWeapon(this CCSPlayerController? player, string weaponname)
    {
        var myWeapons = player?.Pawn.Value?.WeaponServices?.MyWeapons;
        if (myWeapons == null) return false;

        return myWeapons.Any(x => x.Value?.DesignerName == weaponname);
    }

    public static void SetSpeed(this CCSPlayerController? player, float speed)
    {
        var pawn = player?.PlayerPawn.Value;
        if (pawn == null) return;

        pawn.VelocityModifier = speed;
    }

    public static void AddSpeed(this CCSPlayerController? player, float speed)
    {
        var pawn = player?.PlayerPawn.Value;
        if (pawn == null) return;

        pawn.VelocityModifier += speed;
    }

    public static void SetGravity(this CCSPlayerController? player, float gravity)
    {
        var pawn = player?.PlayerPawn.Value;
        if (pawn == null) return;

        pawn.GravityScale = gravity;
    }

    public static void Freeze(this CCSPlayerController? player)
    {
        var pawn = player?.PlayerPawn.Value;
        if (pawn == null) return;

        pawn.MoveType = MoveType_t.MOVETYPE_INVALID;
        Schema.SetSchemaValue(pawn.Handle, "CBaseEntity", "m_nActualMoveType", 11); // invalid
        Utilities.SetStateChanged(pawn, "CBaseEntity", "m_MoveType");
    }

    public static void Unfreeze(this CCSPlayerController? player)
    {
        var pawn = player?.PlayerPawn.Value;
        if (pawn == null) return;

        pawn.MoveType = MoveType_t.MOVETYPE_WALK;
        Schema.SetSchemaValue(pawn.Handle, "CBaseEntity", "m_nActualMoveType", 2); // walk
        Utilities.SetStateChanged(pawn, "CBaseEntity", "m_MoveType");
    }

    public static void ToggleNoclip(this CCSPlayerController? player)
    {
        var pawn = player?.PlayerPawn.Value;
        if (pawn == null) return;

        if (pawn.MoveType == MoveType_t.MOVETYPE_NOCLIP)
        {
            pawn.MoveType = MoveType_t.MOVETYPE_WALK;
            Schema.SetSchemaValue(pawn.Handle, "CBaseEntity", "m_nActualMoveType", 2); // walk
            Utilities.SetStateChanged(pawn, "CBaseEntity", "m_MoveType");
        }
        else
        {
            pawn.MoveType = MoveType_t.MOVETYPE_NOCLIP;
            Schema.SetSchemaValue(pawn.Handle, "CBaseEntity", "m_nActualMoveType", 8); // noclip
            Utilities.SetStateChanged(pawn, "CBaseEntity", "m_MoveType");
        }
    }

    public static void Rename(this CCSPlayerController? player, string value)
    {
        if (player == null) return;
        if (value == "") return;

        player.PlayerName = value;
        Utilities.SetStateChanged(player, "CBasePlayerController", "m_iszPlayerName");
    }

    public static void TeleportTo(this CCSPlayerController? player, CCSPlayerController? target)
    {
        if (player == null || !player.PawnIsAlive) return;
        if (target == null || target.IsValid || !target.PawnIsAlive) return;

        var pos = player.Pawn.Value?.AbsOrigin;
        if(pos == null) return;

        pos.Z += 50f;
        target.Pawn.Value?.Teleport(pos);
    }

    public static void SetModel(this CCSPlayerController? player, string value)
    {
        if (player == null || !player.PawnIsAlive) return;

        player.Pawn?.Value?.SetModel(value);
    }

    public static void CPrintToChat(this CCSPlayerController player, string message)
    {
        player.PrintToChat(Stocks.GetColoredText(message));
    }
}