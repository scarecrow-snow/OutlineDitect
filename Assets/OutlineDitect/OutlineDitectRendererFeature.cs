using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class OutlineDitectRendererFeature : ScriptableRendererFeature
{

    // インスペクターで設定する　アウトライン描画するマテリアル
    // SerializeFieldでインスペクタに設定を公開できる
    [SerializeField] private RenderPassEvent renderPassEvent; // 描画するタイミング

    [SerializeField] private Shader _shader;

    // 実際に描画処理を行うクラス
    private OutlineDitectRenderPass _blitPass;

    // Passを生成する
    public override void Create()
    {
        var material = CoreUtils.CreateEngineMaterial(_shader);
        _blitPass = new OutlineDitectRenderPass(renderPassEvent, material);
    }

    // ScriptableRendererにPassを紐付ける
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        _blitPass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(_blitPass);
    }

}
