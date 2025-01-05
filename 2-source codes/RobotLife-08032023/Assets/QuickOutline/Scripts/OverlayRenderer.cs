using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OverlayRenderer : MonoBehaviour
{
    public enum DepthTests
    {
        LEqual,
        Always
    }

    public enum OutlinePatterns
    {
        Rect,
        Diamond
    }

    private Shader _shader;
    private Material _material;
    public Camera _camera;
    private CommandBuffer _commandBuffer;

    [Header("Settings")]
    public CameraEvent _cameraEvent = CameraEvent.BeforeImageEffects;
    public DepthTests _depthTest = DepthTests.LEqual;
    public OutlinePatterns _outlinePattern = OutlinePatterns.Diamond;
    public int _hoverGroupID = 1;
    public int _selectedGroupID = 1;

    [Header("Colors")]
    public Color _outlineColor = Color.white;
    public Color _fillColor = new Color(0f, 0.6f, 1.0f, 0.4f);

    [Header("Objects")]
    public GameObject _mouseOverObject;
    public GameObject _selectedObject;

    public void OnEnable()
    {
        //if (_camera == null)
         //  _camera = Camera.main;

        _shader = Shader.Find("Hidden/OverlayShader");
        _material = new Material(_shader);
        _commandBuffer = new CommandBuffer();
        //_camera = Camera.main;
        _camera.AddCommandBuffer(_cameraEvent, _commandBuffer);
    }

    public void OnDisable()
    {
        if (_commandBuffer != null && _camera != null)
        {
            _camera.RemoveCommandBuffer(_cameraEvent, _commandBuffer);
            _commandBuffer.Release();
            _commandBuffer = null;
        }
    }

    private static List<MeshFilter> _meshFilters = new List<MeshFilter>();


    RaycastHit[] raycastHits = new RaycastHit[16];
    bool GetGameObjectAtMousePointer(out GameObject gameObject)
    {
        gameObject = null;
        float distanceToObject = float.MaxValue;

        // Get all colliders at the mouse pointer
        // Use RaycastNonAlloc instead of RaycastAll to avoid garbage, as this test runs every update
        var hitCount = Physics.RaycastNonAlloc(_camera.ScreenPointToRay(Input.mousePosition), raycastHits);

        for (int i = 0; i < hitCount; i++)
        {
            var collider = raycastHits[i].collider;
            var distance = raycastHits[i].distance;

            // Check if this object is marked as OverlaySelectable, and if it is closer than previous hits
            // Use GetComponentInParent to allow the colliders on child objects
            var selectable = collider.GetComponentInParent<OverlaySelectable>();
            if (selectable != null && distance < distanceToObject)
            {
                gameObject = selectable.gameObject;
                distanceToObject = distance;
            }
        }

        return gameObject != null;
    }

    void LateUpdate()
    {
        _commandBuffer.Clear();
        // Request an 8 bit single channel texture without depth buffer.
        RenderTexture overlayIDTexture = RenderTexture.GetTemporary(_camera.pixelWidth, _camera.pixelHeight, 0, RenderTextureFormat.R8);
        // Bind the temporary render texture, but keep using the camera's depth buffer
        _commandBuffer.SetRenderTarget(overlayIDTexture, BuiltinRenderTextureType.Depth);
        // Always clear temporary RenderTextures before use, their content is random.
        _commandBuffer.ClearRenderTarget(false, true, Color.clear, 1.0f);

        // update _mouseOverObject
        GetGameObjectAtMousePointer(out _mouseOverObject);
        if (Input.GetMouseButtonDown(0))
            _selectedObject = _mouseOverObject;

        // Pattern Selection
        if (_outlinePattern == OutlinePatterns.Rect)
        {
            _material.DisableKeyword("Pattern_Diamond");
            _material.EnableKeyword("Pattern_Rect");
        }
        else
        {
            _material.DisableKeyword("Pattern_Rect");
            _material.EnableKeyword("Pattern_Diamond");
        }

        // First Pass: Write group id to texture
        if (_selectedObject != null && _commandBuffer != null)
        {
            DrawAllMeshes(_selectedObject, _material, GetShaderPassID(_depthTest, 0));
            _commandBuffer.SetGlobalFloat(ShaderIDs._GroupID, _selectedGroupID);
        }

        if (_mouseOverObject != null && _commandBuffer != null)
        {
            DrawAllMeshes(_mouseOverObject, _material, GetShaderPassID(_depthTest, 0));
            _commandBuffer.SetGlobalFloat(ShaderIDs._GroupID, _hoverGroupID);
        }

        // Bind the camera render target
        _commandBuffer.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, BuiltinRenderTextureType.Depth);
        _commandBuffer.SetGlobalTexture(ShaderIDs._OverlayIDTexture, overlayIDTexture);

        // Second Pass: Apply overlay fill and outline
        if (_selectedObject != null && _commandBuffer != null)
        {
            _commandBuffer.SetGlobalVector(ShaderIDs._OutlineColor, _outlineColor);
            _commandBuffer.SetGlobalVector(ShaderIDs._FillColor, _fillColor);
            DrawAllMeshes(_selectedObject, _material, GetShaderPassID(_depthTest, 1));
        }

        if (_mouseOverObject != null && _commandBuffer != null)
        {
            _commandBuffer.SetGlobalVector(ShaderIDs._OutlineColor, _outlineColor);
            _commandBuffer.SetGlobalVector(ShaderIDs._FillColor, _fillColor);
            DrawAllMeshes(_mouseOverObject, _material, GetShaderPassID(_depthTest, 1));
        }

        // Don't forget to release the temporary render texture
        RenderTexture.ReleaseTemporary(overlayIDTexture);
    }

    private void DrawAllMeshes(GameObject gameObject, Material material, int pass)
    {
        _meshFilters.Clear();
        gameObject.GetComponentsInChildren(_meshFilters);

        foreach (var meshFilter in _meshFilters)
        {
            // Static objects may use static batching, preventing us from accessing their default mesh
            if (!meshFilter.gameObject.isStatic)
            {
                var mesh = meshFilter.sharedMesh;
                // Render all submeshes
                for (int i = 0; i < mesh.subMeshCount; i++)
                    _commandBuffer.DrawMesh(mesh, meshFilter.transform.localToWorldMatrix, material, i, pass);
            }
        }
    }

    public int GetShaderPassID(DepthTests depth, int step)
    {
        if (depth == DepthTests.LEqual)
            return step;
        else
            return step + 2;
    }

    // Shader variable IDs
    public static class ShaderIDs
    {
        public static readonly int _GroupID = Shader.PropertyToID("_GroupID");
        public static readonly int _OverlayIDTexture = Shader.PropertyToID("_OverlayIDTexture");
        public static readonly int _FillColor = Shader.PropertyToID("_FillColor");
        public static readonly int _OutlineColor = Shader.PropertyToID("_OutlineColor");
    }
}
