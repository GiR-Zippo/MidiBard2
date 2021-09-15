﻿using System.Numerics;
using Dalamud.Interface;
using ImGuiNET;

namespace MidiBard
{
	public static class ImguiUtil
	{
		public static void HelpMarker(string desc, bool sameline = true)
		{
			if (sameline) ImGui.SameLine();
			//ImGui.PushFont(UiBuilder.IconFont);
			ImGui.TextDisabled("(?)");
			//ImGui.PopFont();
			if (ImGui.IsItemHovered())
			{
				ImGui.PushFont(UiBuilder.DefaultFont);
				ImGui.BeginTooltip();
				ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
				ImGui.TextUnformatted(desc);
				ImGui.PopTextWrapPos();
				ImGui.EndTooltip();
				ImGui.PopFont();
			}
		}

		public static bool IconButton(FontAwesomeIcon icon, string id)
		{
			ImGui.PushFont(UiBuilder.IconFont);
			var ret = ImGui.Button($"{icon.ToIconString()}##{id}");
			ImGui.PopFont();
			return ret;
		}

		public static void ToolTip(string desc)
		{
			if (ImGui.IsItemHovered())
			{
				ImGui.PushFont(UiBuilder.DefaultFont);
				ImGui.BeginTooltip();
				ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
				ImGui.TextUnformatted(desc);
				ImGui.PopTextWrapPos();
				ImGui.EndTooltip();
				ImGui.PopFont();
			}
		}

		public static unsafe void DrawColoredBanner(uint color, string content)
		{
			ImGui.PushStyleColor(ImGuiCol.Button, color);
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, color);
			ImGui.Button(content, new Vector2(-1, ImGui.GetFrameHeight()));
			ImGui.PopStyleColor(2);
		}

		public const uint ColorRed = 0xFF0000C8;
		public const uint ColorYellow = 0xFF00C8C8;
		public const uint orange = 0xAA00B0E0;
		public const uint red = 0xAA0000D0;
		public const uint grassgreen = 0x9C60FF8E;
		public const uint alphaedgrassgreen = 0x3C60FF8E;
		public const uint darkgreen = 0xAC104020;
		public const uint violet = 0xAAFF888E;
	}
}