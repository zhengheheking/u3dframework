using Events;
using System;
public class UIWindowCtrl
{
    public virtual Type GetWindowType()
    {
        return null;
    }
    public virtual void OnCreate(object arg)
    {
       
    }
    public virtual void ShowWindow(UIWindow.WindowEffectFinished finishEffectFunc)
    {
       
    }
    public virtual void HideWindow(UIWindow.WindowEffectFinished finishEffectFunc)
    {

    }
    public virtual void ShowWindow()
    {
        
    }
    public virtual void HideWindow()
    {
       
    }
    public virtual void OnUpdate()
    {
        
    }
    public virtual void OnDestory() 
    {
        
    }
    public virtual void ReleaseWindow()
    {

    }
    public virtual UIWindow GetWindow()
    {
        return null;
    }
    public GameDispatcher getDispatcher()
    {
        return GameDispatcher.Instance;
    }
}
public class UIWindowCtrlTemplate<TUI> : UIWindowCtrl where TUI : UIWindow
{
    protected TUI LogicUI { get; private set; }
    public UIWindowCtrlTemplate()
    {
        LogicUI = null;
    }
    public override Type GetWindowType()
    {
        return typeof(TUI);
    }
    public override void OnCreate(object arg)
    {
        LogicUI = arg as TUI;
        LogicUI.OnCreate();
    }
    public override void ShowWindow(UIWindow.WindowEffectFinished finishEffectFunc)
    {
        LogicUI.ShowWindow(finishEffectFunc);
    }
    public override void HideWindow(UIWindow.WindowEffectFinished finishEffectFunc)
    {
        LogicUI.HideWindow(finishEffectFunc);
    }
    public override void ShowWindow()
    {
        LogicUI.ShowWindow();
    }
    public override void HideWindow()
    {
        LogicUI.HideWindow();
    }
    public override void OnUpdate()
    {
        LogicUI.OnUpdate();
    }
    public override void OnDestory() 
    {
        LogicUI.OnDestory();
    }
    public override void ReleaseWindow()
    {
        LogicUI.ReleaseWindow();
    }
    public override UIWindow GetWindow()
    {
        return LogicUI;
    }
}