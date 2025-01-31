﻿// Copyright (C) 2022 akira0245
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see https://github.com/akira0245/MidiBard/blob/master/LICENSE.
// 
// This code is written by akira0245 and was originally used in the MidiBard project. Any usage of this code must prominently credit the author, akira0245, and indicate that it was originally used in the MidiBard project.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dalamud.Plugin.Services;
using static Dalamud.api;

namespace MidiBard.Managers;

public class PartyWatcher : IDisposable
{
    public PartyWatcher()
    {
        api.Framework.Update += Framework_Update;
    }

    public long[] PartyMemberCIDs { get; private set; } = Array.Empty<long>();

    public static long[] GetMemberCIDs()
    {
        System.Collections.Generic.List<long> cids = new();
        foreach(var p in api.PartyList)
        {
            try
            {
                if (p.ObjectId <= 0 || !p.GameObject.IsValid())
                    continue;
                if (p.World.Value.RowId > 0 && p.Territory.Value.RowId > 0)
                {
                    cids.Add(p.ContentId);
                }
            }
            catch (NullReferenceException) { }
        }
        return cids.ToArray();

    }

    [SuppressMessage("ReSharper", "SimplifyLinqExpressionUseAll")]
    private void Framework_Update(IFramework framework)
    {
        var newMemberCIDs = GetMemberCIDs();
        if (!newMemberCIDs.ToHashSet().SetEquals(PartyMemberCIDs.ToHashSet()))
        {
            //PluginLog.Warning($"CHANGE {newList.Length - PartyMembers.Length}");
            //PluginLog.Information("OLD:\n"+string.Join("\n", PartyMembers.Select(i=>$"{i.Name} {i.ContentId:X}")));
            //PluginLog.Information("NEW:\n"+string.Join("\n", newList.Select(i=>$"{i.Name} {i.ContentId:X}")));

            foreach (var cid in newMemberCIDs)
            {
                if (!PartyMemberCIDs.Any(i => i == cid))
                {
                    PluginLog.Debug($"JOIN {cid}");
                    PartyMemberJoin?.Invoke(this, cid);
                }
            }

            foreach (var partyMember in PartyMemberCIDs)
            {
                if (!newMemberCIDs.Any(i => i == partyMember))
                {
                    PluginLog.Debug($"LEAVE {partyMember}");
                    PartyMemberLeave?.Invoke(this, partyMember);
                }
            }
        }

        PartyMemberCIDs = newMemberCIDs;
    }

    public static event EventHandler<long> PartyMemberJoin;
    public static event EventHandler<long> PartyMemberLeave;

    public void Dispose()
    {
        api.Framework.Update -= Framework_Update;
        PartyMemberJoin = delegate { };
        PartyMemberLeave = delegate { };
    }
}