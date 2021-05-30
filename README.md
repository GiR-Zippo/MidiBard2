# **MidiBard**

MidiBard是基于[卫月框架](https://bbs.tggfl.com/topic/32/dalamud-%E5%8D%AB%E6%9C%88%E6%A1%86%E6%9E%B6)的强大且易用的诗人midi演奏插件。目前版本兼容国服及国际服。

# 主要特性
* 无需繁琐的键位配置，打开即用。
* 毫秒精度的音符回放和零按键延迟，最大程度还原乐曲细节。
* 超出音域音符的自适应功能，节省适配midi文件的时间。
* 内置播放列表，一次导入演奏所有你喜爱的乐曲。
* 多音轨选择功能，可以同时选择任意个音轨演奏或合奏。
* 免去配置时间，使用合奏助手实现一键高精度合奏。
* 随时查看当前演奏的乐器，通过界面或命令一键切换乐器，也可以随不同乐曲自动切换对应的乐器。


# 插件界面
[![gwaZJ1.png](https://z3.ax1x.com/2021/05/12/gwaZJ1.png)](https://imgtu.com/i/gwaZJ1) [![g0C64x.png](https://z3.ax1x.com/2021/05/12/g0C64x.png)](https://imgtu.com/i/g0C64x)

# 安装方法
> MidiBard需要[卫月框架](https://bbs.tggfl.com/topic/32/dalamud-%E5%8D%AB%E6%9C%88%E6%A1%86%E6%9E%B6)，如未安装请参考[原帖](https://bbs.tggfl.com/topic/32/dalamud-%E5%8D%AB%E6%9C%88%E6%A1%86%E6%9E%B6)安装后继续。

正确安装卫月框架并注入后在游戏聊天框中输入`/xlsettings`打开Dalamud 设置窗口，复制该源  
`https://raw.fastgit.org/akira0245/DalamudPlugins/cn/pluginmaster.json` 并将其添加到插件仓库  

[![gw7vxx.png](https://z3.ax1x.com/2021/05/12/gw7vxx.png)](https://imgtu.com/i/gw7vxx)

成功添加后在`/xlplugins`中搜索MidiBard并安装即可。


# 使用FAQ
* **如何开始使用MIDIBARD演奏？**  
MIDIBARD窗口默认在角色进入演奏模式后自动弹出。点击窗口左上角的“+”按钮来将乐曲文件导入到播放列表。仅支持.mid格式的乐曲。导入时按Ctrl或Shift可以选择多个文件一同导入。双击播放列表中要演奏的乐曲后点击播放按钮开始演奏。

* **为什么点击播放之后没有正常演奏？**  
MIDIBARD仅使用37键演奏模式。请在游戏“乐器演奏操作设置”的“键盘操作”类别下启用“全音阶一同显示、设置按键”的选项。

* **如何使用MIDIBARD进行多人合奏？**  
MIDIBARD使用游戏中的合奏助手来完成合奏，请在合奏时打开游戏的节拍器窗口。合奏前在播放列表中双击要合奏的乐曲，播放器下方会出现可供演奏的所有音轨，请为每位合奏成员分别选择其需要演奏的音轨。选择音轨后队长点击节拍器窗口的“合奏准备确认”按钮，并确保合奏准备确认窗口中已勾选“使用合奏助手”选项后点击开始即可开始合奏。  
注：节拍器前两小节为准备时间，从第1小节开始会正式开始合奏。考虑到不同使用环境乐曲加载速度可能不一致，为了避免切换乐曲导致的不同步，在乐曲结束时合奏会自动停止。

* **后台演奏时有轻微卡顿不流畅怎么办？**  
在游戏内 *系统设置→显示设置→帧数限制* 中取消勾选 *“程序在游戏窗口处于非激活状态时限制帧数”* 并应用设置。

---
> 本项目遵循 GNU Affero General Public License v3.0 协议开源。  
> 项目源码可在 https://github.com/akira0245/MidiBard 查看（写的很烂被迫开源  
> 欢迎补充英文翻译和pr


# 其他问题

> 有bug或建议或讨论加qq群解决：260985966
