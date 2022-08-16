using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlineDitectRenderPass : ScriptableRenderPass
{
    private const string CommandBufferName = nameof(OutlineDitectRenderPass);

    // 描画に使用するマテリアル
    private readonly Material _material;

    // カメラの出力先テクスチャを示す識別子
    private RenderTargetIdentifier _currentTarget;

    // Blitの際、途中経過を保存するテクスチャのハンドル
    private RenderTargetHandle _tempTexture;

    // コンストラクタ
    public OutlineDitectRenderPass(RenderPassEvent renderPassEvent, Material blitMaterial)
    {
        this.renderPassEvent = renderPassEvent;
        _material = blitMaterial;
        _tempTexture = new RenderTargetHandle();
        _tempTexture.Init("_TemporaryColorTexture");
       // _tempTexture.Init("_CameraDepthTexture");
    }

    // 初期化
    public void Setup(RenderTargetIdentifier sourceColor)
    {
        _currentTarget = sourceColor;
    }

    // Blitを実行する
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        // CommandBufferを取得
        var commandBuffer = CommandBufferPool.Get(CommandBufferName);


        // CameraのTargetと同じ設定のテクスチャを新しく取得する
        commandBuffer.GetTemporaryRT(_tempTexture.id, renderingData.cameraData.cameraTargetDescriptor);

        // 指定されたマテリアルを用いて、カメラの出力を一時的なテクスチャへ転写
        Blit(commandBuffer, _currentTarget, _tempTexture.Identifier(), _material, 0);

        // 一時的なテクスチャからカメラの出力へ描画結果を戻す
        Blit(commandBuffer, _tempTexture.Identifier(), _currentTarget);

        // 一時的なテクスチャを開放
        commandBuffer.ReleaseTemporaryRT(_tempTexture.id);

        // CommandBufferを実行
        context.ExecuteCommandBuffer(commandBuffer);
        //context.Submit();

        // CommandBufferを開放
        CommandBufferPool.Release(commandBuffer);
    }

}
