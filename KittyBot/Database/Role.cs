using System;
using System.Collections.Generic;
using System.Text;

namespace KittyBot.Database
{
    public static class Role
    {
        private static Dictionary<string, List<RoleSetting>> _roleSettings;

        public static void AddReaction(string messageId, RoleSetting setting) {
            if(_roleSettings.TryGetValue(messageId, out var settings)) {
                settings.Add(setting);
            } else {
                _roleSettings.Add(messageId, [setting]);
            }
        }

        public static void RemoveReaction(string messageId, RoleSetting setting) {
            if(_roleSettings.TryGetValue(messageId, out var settings)) {
                for(int i = 0; i < settings.Count; i++) {
                    if(settings[i].EmojiName == setting.EmojiName && settings[i].EmojiId == setting.EmojiId) {
                        settings.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }

    public struct RoleSetting
    {
        public string EmojiId;
        public string EmojiName;
        public string Role;
    }
}
