// Copyright (C) 2022 akira0245
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
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Dalamud.Plugin.Services;
using Dalamud.Utility;

namespace MidiBard.Managers;

public class PartyWatcher : IDisposable
{
    public record PartyMemberInfo(string Name, uint EntityId, ulong ContentId, uint WorldId, string World, uint ClassJobId, byte Level);

    /// <summary>
    /// Get the PartyList
    /// </summary>
    public IReadOnlyList<PartyMemberInfo> PartyMembers => Volatile.Read(ref _partyMembers);

    /// <summary>
    /// Are we in a party
    /// </summary>
    public bool IsInParty => Volatile.Read(ref _isInParty);

    /// <summary>
    /// Indicates if we are the partylead
    /// </summary>
    public bool IsPartyLeader => Volatile.Read(ref _isPartyLeader);


    private PartyMemberInfo[] _partyMembers = Array.Empty<PartyMemberInfo>();
    private bool _isInParty;
    private bool _isPartyLeader;

    private static readonly Lazy<PartyWatcher> _instance = new(() => new PartyWatcher());
    public static PartyWatcher Instance => _instance.Value;
    private bool started { get; set; } = false;

    private PartyWatcher() { }

    /// <summary>
    /// Start the singleton
    /// </summary>
    public void Start()
    {
        if (started)
            return;
        api.Framework.Update += Framework_Update;
        started = true;
    }

    /// <summary>
    /// Stop the singleton and cleanup
    /// </summary>
    public void Dispose()
    {
        if (!started) return;
        api.Framework.Update -= Framework_Update;
        Volatile.Write(ref _partyMembers, Array.Empty<PartyMemberInfo>());
    }

    /// <summary>
    /// Called by Frameowrk Update
    /// </summary>
    /// <param name="framework"></param>
    private void Framework_Update(IFramework framework)
    {
        var oldMembers = _partyMembers;
        var newMembers = api.PartyList
            .Select(m => new PartyMemberInfo(
                m.Name.TextValue,
                m.EntityId,
                m.ContentId,
                m.World.RowId,
                m.World.ValueNullable?.Name.ToDalamudString().TextValue ?? "",
                m.ClassJob.RowId,
                m.Level))
            .ToArray();

        var oldCIDs = oldMembers.Select(m => m.ContentId).ToHashSet();
        var newCIDs = newMembers.Select(m => m.ContentId).ToHashSet();

        var joined = newCIDs.Except(oldCIDs).ToArray();
        var left = oldCIDs.Except(newCIDs).ToArray();
        bool isInParty = api.PartyList?.Length > 1;
        var lead = isInParty ? api.PartyList[(int)api.PartyList.PartyLeaderIndex] : null;

        Volatile.Write(ref _isInParty, isInParty);
        Volatile.Write(ref _isPartyLeader, isInParty && api.Player.ContentId == lead?.ContentId);
        Volatile.Write(ref _partyMembers, newMembers);
    }
}
