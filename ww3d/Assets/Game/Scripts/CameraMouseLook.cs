using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))] // Скрипт сам проверит наличие компонента Camera
public class CameraMouseLook : MonoBehaviour {

    [Header("Rotation")]
    public float sensitivity = 0.1f;
    public float smoothing = 15f;
    [Header("Zoom")]
    public float zoomSensitivity = 0.1f;
    public float zoomSmoothing = 10f;
    public float minHeight = 5f;
    public float maxHeight = 30f;
    private Camera _cam;
    private float _targetFOV;
    private Vector2 _currentRotation;
    private Vector2 _smoothedDelta;
    private float _targetHeight;

    private void Start() {
        _cam = GetComponent<Camera>();
        _targetFOV = _cam.fieldOfView; // Запоминаем текущий FOV как стартовый

        // Инициализируем вращение из редактора
        var existingRotation = transform.localRotation.eulerAngles;
        _currentRotation.x = existingRotation.y;
        _currentRotation.y = existingRotation.x;

        if (_currentRotation.y > 180f) {
            _currentRotation.y -= 360f;
        }

        _targetHeight = transform.localPosition.y;
    }

    private void Update() {
        HandleRotation();
        HandleZoom();
    }

    private void HandleRotation() {
        if (Mouse.current == null) {
            return;
        }

        var rawDelta = Vector2.zero;

        // Если кнопка зажата — берем дельту из мыши
        if (Mouse.current.rightButton.isPressed) {
            rawDelta = Mouse.current.delta.ReadValue();
        }

        // Lerp работает всегда: 
        // Если кнопка зажата — стремится к rawDelta (движение)
        // Если отпущена — стремится к Vector2.zero (плавное затухание)
        _smoothedDelta = Vector2.Lerp(_smoothedDelta, rawDelta, Time.deltaTime * smoothing);

        // Применяем накопленное сглаженное значение, даже если кнопка отпущена
        _currentRotation.x += _smoothedDelta.x * sensitivity;
        _currentRotation.y -= _smoothedDelta.y * sensitivity;
        _currentRotation.y = Mathf.Clamp(_currentRotation.y, 40f, 80f);

        transform.localRotation = Quaternion.Euler(_currentRotation.y, _currentRotation.x, 0f);
    }

    // private void HandleRotation() {
    //     if (Mouse.current != null && Mouse.current.rightButton.isPressed) {
    //         var mouseDelta = Mouse.current.delta.ReadValue();
    //         _smoothedDelta = Vector2.Lerp(_smoothedDelta, mouseDelta, Time.deltaTime * smoothing);
    //
    //         _currentRotation.x += _smoothedDelta.x * sensitivity;
    //         _currentRotation.y -= _smoothedDelta.y * sensitivity;
    //         _currentRotation.y = Mathf.Clamp(_currentRotation.y, 40f, 80f);
    //
    //         transform.localRotation = Quaternion.Euler(_currentRotation.y, _currentRotation.x, 0f);
    //         // Cursor.lockState = CursorLockMode.Locked;
    //     } else {
    //         // Cursor.lockState = CursorLockMode.None;
    //         _smoothedDelta = Vector2.zero;
    //     }
    // }

    private void HandleZoom() {
        if (Mouse.current == null) {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        // Получаем прокрутку
        var scrollDelta = Mouse.current.scroll.ReadValue().y;

        if (Mathf.Abs(scrollDelta) > 0.1f) {
            // Вычисляем новую целевую высоту
            // Инвертируем scrollDelta (минус), чтобы прокрутка вперед (вверх) опускала камеру
            _targetHeight -= scrollDelta * zoomSensitivity;

            // Ограничиваем высоту по заданным лимитам
            _targetHeight = Mathf.Clamp(_targetHeight, minHeight, maxHeight);
        }

        // Плавно перемещаем камеру только по оси Y
        var currentPos = transform.position;
        var smoothedY = Mathf.Lerp(currentPos.y, _targetHeight, Time.deltaTime * zoomSmoothing);
        // Debug.Log(smoothedY);
        transform.position = new Vector3(currentPos.x, smoothedY, currentPos.z);
    }

    // private void HandleZoomFOV() {
    //     if (Mouse.current == null) {
    //         return;
    //     }
    //
    //     // В New Input System значение прокрутки находится в .scroll.ReadValue()
    //     // Обычно это (0, 120) или (0, -120) за один "щелчок"
    //     var scrollDelta = Mouse.current.scroll.ReadValue().y;
    //
    //     if (Mathf.Abs(scrollDelta) > 0.1f) {
    //         // Изменяем целевой FOV (инвертируем, чтобы скролл вперед приближал)
    //         _targetFOV -= scrollDelta * zoomSensitivity;
    //         _targetFOV = Mathf.Clamp(_targetFOV, minFOV, maxFOV);
    //     }
    //
    //     // Плавное изменение текущего FOV камеры к целевому
    //     _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _targetFOV, Time.deltaTime * zoomSmoothing);
    // }

}

/*
public class CameraMouseLook : MonoBehaviour
{
    public float sensitivity = 2.0f; // Чувствительность мыши
    public float smoothing = 2.0f;    // Сглаживание

    private Vector2 _currentRotation;
    private Vector2 _smoothVelocity;

    void Update()
    {
        // Проверяем, зажата ли левая кнопка мыши (0 - ЛКМ)
        if (Input.GetMouseButton(0))
        {
            // Получаем движение мыши
            Vector2 mouseInput = new Vector2(
                Input.GetAxisRaw("Mouse X"),
                Input.GetAxisRaw("Mouse Y")
            );

            // Умножаем на чувствительность
            mouseInput = Vector2.Scale(mouseInput, new Vector2(sensitivity, sensitivity));

            // Плавный переход (интерполяция)
            _smoothVelocity.x = Mathf.Lerp(_smoothVelocity.x, mouseInput.x, 1f / smoothing);
            _smoothVelocity.y = Mathf.Lerp(_smoothVelocity.y, mouseInput.y, 1f / smoothing);

            _currentRotation += _smoothVelocity;

            // Ограничиваем вращение по вертикали (вверх/вниз), чтобы не сделать "сальто"
            _currentRotation.y = Mathf.Clamp(_currentRotation.y, -80f, 80f);

            // Применяем вращение к камере
            // По оси X (вращение камеры влево-вправо) используем Mouse X
            // По оси Y (вращение камеры вверх-вниз) используем Mouse Y (инвертируем, чтобы было привычно)
            transform.localRotation = Quaternion.AngleAxis(_currentRotation.x, Vector3.up) *
                                      Quaternion.AngleAxis(-_currentRotation.y, Vector3.right);
        }
    }
}
*/