## 视频讲解

https://www.bilibili.com/video/BV1LD421s7Dn/?spm_id_from=333.788

讲解：
1.如何自定义一条轨道
- 需要加哪些特性（Color、Name、Binding、Serialize，Clip类一定是单独文件，且文件名称与类名一致(因为是ScriptableObject)）
- 如何定义Clip的AEPlayableBehaviour驱动IEditorBehaviour, IRuntimeBehaviour
- 如何自定义样式AEClipStyle，其中类名一定要写全，OverrideUI决定是覆盖还是添加
  2.动画轨道的StartPosition，用复制贴快捷键

## 创建资产

通过右键Create、AETimeline编辑器、AETimelineAssets创建资产。

![](ImagesAssets/Pasted%20image%2020240503172205.png)

## 打开窗口

通过菜单项Tools、AETimeline编辑器打开编辑器窗口。

![](ImagesAssets/Pasted%20image%2020240503172328.png)

## 选择资产

如图中左边ObjectField是AETimelineAssets附加到的对象，右边选择AETimelineAssets资产。

![](ImagesAssets/Pasted%20image%2020240503172433.png)
## 编辑

### 右键

通过右键创建轨道与Clip项，如图所示，右键轨道头部分空白处，弹出上下文菜单，选择对应轨道创建。

![](ImagesAssets/Pasted%20image%2020240503172754.png)

同理右键轨道头部，弹出上下文菜单，删除右键的轨道。

![](ImagesAssets/Pasted%20image%2020240503172849.png)

右键轨道体空白处，在对应轨道体中添加Clip项。

![](ImagesAssets/Pasted%20image%2020240503173002.png)

### 快捷键

操作Clip的快捷键有下面几种：

Ctrl+C：复制对应Clip

Ctrl+V：粘贴复制的Clip到鼠标位置

Ctrl+X：剪切对应Clip

Delete：删除对应Clip

### 操作Clip

用鼠标拖动Clip可以改变Clip起始位置。

将鼠标放在Clip的尾部，鼠标显示为Resize样式的时候，可以拖动改变Clip大小。

### 鼠标中键

鼠标中键在轨道右侧部分，可拖动轨道左右移动。

## 自定义轨道

(AEAnimationTrack)

自定义轨道需要继承自标准轨道StandardTrack，自定义Clip需要继承自StandardClip，我们用特性AEBindClip将这两个类联系到一起。

继承自StandardTrack的类，会自动加入创建轨道上下文菜单中。

注意，在编写Clip类的时候，一定要讲Clip类编写为单独文件，且文件名称与类名一致

![](ImagesAssets/Pasted%20image%2020240503190400.png)

### 必要特性

下面这些特性是编写自定义轨道的时候必须要写的：

AETrackName：用来描述轨道名称的特性

AETrackColor：用来描述轨道颜色的特性

AEBindClip：用来描述轨道的对应Clip类型的特性

Serializable：用来标记为可序列化的特性

![](ImagesAssets/Pasted%20image%2020240503173908.png)

### 自定义样式AEClipStyleAttribute

(AEAnimationTrack、CustomAnimationClip)

当用户需要自定义Clip的样式的时候可以使用AEClipStyleAttribute特性，这个特性用来描述编写了自定义Clip样式函数的类。

使用时，传入自定义样式函数的类名。例如：AEAnimationTrack在CustomAnimationClip类中编写了自定义Clip函数。于是我们传入了AE_SkillEditor_Plus.Excample.BuiltTracks.CustomAnimationClip作为ClassName。

注意，用户编写的其他自定义Clip样式的函数，一定要是静态函数，并且参数和CustomAnimationClip.UpdateUI一模一样否则会报警告，并且自定义函数不生效。

![](ImagesAssets/Pasted%20image%2020240503190945.png)

![](ImagesAssets/Pasted%20image%2020240503191332.png)

其中类名一定要写全，OverrideUI决定是覆盖原来的UI，还是在原来的UI基础上继续绘制。

![](ImagesAssets/Pasted%20image%2020240503170103.png)

自定义的样式类一定要放在Editor下。

![](ImagesAssets/Pasted%20image%2020240503173825.png)

### 轨道驱动

(AEAnimationEditorBehaviour)

![](ImagesAssets/Pasted%20image%2020240503173908.png)

需要Track类继承 IEditorBehaviour, IRuntimeBehaviour，并且分别实现CreateEditorBehaviour和CreateRuntimeBehaviour方法，返回对应AEPlayableBehaviour。

#### AEPlayableBehaviour

![](ImagesAssets/Pasted%20image%2020240503195339.png)

AEPlayableBehaviour中有生命周期函数：OnEnter、Tick、OnExit，供用户自定义Clip的行为。

要注意的是，子类重写OnEnter、OnExit方法一定要调用基类的对应方法。

我们在构造函数中拿到对应的Clip并且保存下来，在生命周期函数中就可以得到对应的Clip了。

例如：在AEAnimationEditorBehaviour中我们在构造函数中拿到了AEAnimationClip，在当前Clip的运行时间内，每一帧都都会调用Tick，我们在Tick中使用SampleAnimation来播放每一帧。

##  Runtime模式

![](ImagesAssets/Pasted%20image%2020240503173908.png)

要在Runtime下运行对应轨道，一定要实现IRuntimeBehaviour接口。具体的AEPlayableBehaviour实现在上面已经讲过了。

![](ImagesAssets/Pasted%20image%2020240503174758.png)

AETimelineTick是运行时的驱动器，我们要播放一个资产需要调用AETimelineTick.PlayAsset，传入对应的资产。

并且在Update函数中更新驱动器。

## 内置轨道

### 动画轨道

![](ImagesAssets/Pasted%20image%2020240503185358.png)

在动画轨道中，Clip超出原本动画范围后，有一个红色的分界线表示动画的结束位置。这些是使用自定义Clip样式实现的，也就是上面提到的AEClipStyleAttribute。当刚刚好的时候分界线是绿色的。

为了在编辑器模式下使得多段动画的位移正常，使用了StartPosition记录本段Clip开始的位置。

![](ImagesAssets/Pasted%20image%2020240503191938.png)

在播放到上一个动画末尾时，选中人物的Position，我们用Ctrl+C复制对应人物的Position，再回到Clip面板选中StartPositon，Ctrl+V，就可以粘贴对应位置。

![](ImagesAssets/Pasted%20image%2020240503192154.png)

## 特效轨道声效轨道

特效轨道和声效轨道没有什么好讲的，正常使用即可。

注意，如果声效没有音量注意看Volume参数是否为0

特效轨道中注意Follow参数勾上后，特效会和gameObject保持Position的相对距离。
