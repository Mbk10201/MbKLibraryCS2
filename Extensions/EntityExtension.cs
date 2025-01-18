using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory;
using System.Drawing;

namespace MBKLibrary.Extensions;

public static partial class EntityExtension
{
    public static float GetDistanceTo(this CBaseEntity entity, CBaseEntity target)
    {
        var basepos = entity.AbsOrigin;
        var targetpos = target.AbsOrigin;

        // Calculate distance
        return MathExtension.GetDistance(basepos, targetpos);
    }

    public static void SetHealth(this CBaseEntity entity, int health)
    {
        Utilities.SetStateChanged(entity, "CBaseEntity", "m_iHealth");
    }

    public static void Detonate(this CBaseEntity entity)
    {
        if (!entity.IsValid)
            return;

        //var explosion = BaseEntity.Create("env_explosion");
        var explosion = Utilities.CreateEntityByName<CEnvExplosion>("env_explosion");
        if(explosion != null && explosion.IsValid)
        {
            explosion.Spawnflags = 16384;
            explosion.Magnitude = 700;
            explosion.RadiusOverride = 350;

            explosion.DispatchSpawn();

            explosion.Teleport(entity.AbsOrigin);

            if (entity is CBasePlayerPawn pawn)
                explosion.TeamNum = pawn.Controller.Value.TeamNum;

            explosion.AcceptInput("Explode");
            explosion.AcceptInput("Kill");

        }
        //     if (explosion?.IsValid == true)
        //     {
        //         explosion.SetProp(PropType.Data, "m_spawnflags", 16384);
        //         explosion.SetProp(PropType.Data, "m_iMagnitude", 200);
        //         explosion.SetProp(PropType.Data, "m_iRadiusOverride", 1600);
        //
        //
        //         var activateEntity = VirtualFunction.Create<IntPtr>(explosion.Handle, 38);
        //         activateEntity(explosion.Handle);
        //
        //         var dest = new Vector(ent.AbsOrigin.X, ent.AbsOrigin.Y, ent.AbsOrigin.Z);
        //
        //         var teleportEntity =
        //             VirtualFunction.Create<IntPtr, IntPtr, IntPtr, IntPtr, bool>(explosion.Handle, 114);
        //         teleportEntity(explosion.Handle, dest.Handle, IntPtr.Zero, IntPtr.Zero, false);
        //
        //         explosion.SetPropEnt(PropType.Send, "m_hOwnerEntity", Player.Index);
        //         explosion.Team = Player.Team;
        //
        //         explosion.Spawn();
        //
        //         explosion.AcceptInput("InputExplode");
        //
        //         Sound.EmitSoundToAll("weapons/hegrenade/explode3.wav", 0, origin: explosion.AbsOrigin,
        //             level: SoundLevel.SNDLVL_120dB);
        //
        //         ent.AcceptInput("InputKill");
        //     }
    }

    public static void Lock(this CBaseEntity entity)
    {
        Schema.SetSchemaValue(entity.Handle, "CBaseModelEntity", "m_bLocked", true);
        Utilities.SetStateChanged(entity, "CBaseModelEntity", "m_bLocked");
    }

    public static bool IsLocked(this CBaseEntity entity)
    {
        return Schema.GetSchemaValue<bool>(entity.Handle, "CBaseModelEntity", "m_bLocked");
    }

    public static void Unlock(this CBaseEntity entity)
    {
        Schema.SetSchemaValue(entity.Handle, "CBaseModelEntity", "m_bLocked", false);
        Utilities.SetStateChanged(entity, "CBaseModelEntity", "m_bLocked");
    }

    public static void SetRenderColor(this CBaseEntity entity, Color color)
    {
        Server.NextFrame(() =>
        {
            var finalEnt = entity.As<CBaseModelEntity>();
            finalEnt.Render = Color.Red;
            Utilities.SetStateChanged(finalEnt, "CBaseModelEntity", "m_clrRender");
        });
    }
}
