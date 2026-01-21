using UnityEngine;
using UnityEngine.InputSystem;

// Важно для новой системы

public class CameraMovement : MonoBehaviour {

    [Header("Настройки движения")]
    public float speed = 10f;
    public float sprintMultiplier = 2f;
    public float smoothing = 10f;
    private Vector3 _targetVelocity;
    private Vector3 _currentVelocity;

    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        if (Keyboard.current == null) {
            return;
        }

        var inputVector = Vector3.zero;

        // Получаем векторы направления, обнуляя вертикальную составляющую (Y)
        var forward = transform.forward;
        forward.y = 0; // Обнуляем наклон, чтобы не лететь вверх/вниз
        forward.Normalize(); // Возвращаем длину вектора к 1

        var right = transform.right;
        right.y = 0;
        right.Normalize();

        // Вперед и Назад (теперь только по Z и X)
        if (Keyboard.current.upArrowKey.isPressed) {
            inputVector += forward;
        }

        if (Keyboard.current.downArrowKey.isPressed) {
            inputVector -= forward;
        }

        // Влево и Вправо
        if (Keyboard.current.leftArrowKey.isPressed) {
            inputVector -= right;
        }

        if (Keyboard.current.rightArrowKey.isPressed) {
            inputVector += right;
        }

        var currentSpeed = speed;
        if (Keyboard.current.leftShiftKey.isPressed) {
            currentSpeed *= sprintMultiplier;
        }

        _targetVelocity = inputVector.normalized * currentSpeed;
        _currentVelocity = Vector3.Lerp(_currentVelocity, _targetVelocity, Time.deltaTime * smoothing);

        transform.position += _currentVelocity * Time.deltaTime;

        /*
        if (Keyboard.current == null) {
            return;
        }

        var inputVector = Vector3.zero;

        // Влево и Вправо (по горизонтальной оси камеры)
        if (Keyboard.current.leftArrowKey.isPressed) {
            inputVector -= transform.right;
        }

        if (Keyboard.current.rightArrowKey.isPressed) {
            inputVector += transform.right;
        }

        // Вверх и Вниз (теперь по вертикальной оси камеры, а не вперед-назад)
        if (Keyboard.current.upArrowKey.isPressed) {
            inputVector += transform.forward;
        }

        if (Keyboard.current.downArrowKey.isPressed) {
            inputVector -= transform.forward;
        }

        var currentSpeed = speed;
        if (Keyboard.current.leftShiftKey.isPressed) {
            currentSpeed *= sprintMultiplier;
        }

        // Вычисляем скорость (normalized, чтобы по диагонали не летела быстрее)
        _targetVelocity = inputVector.normalized * currentSpeed;

        // Плавность
        _currentVelocity = Vector3.Lerp(_currentVelocity, _targetVelocity, Time.deltaTime * smoothing);

        // Применяем перемещение
        transform.position += _currentVelocity * Time.deltaTime;
        */
    }

}

/*
public class CameraMovement : MonoBehaviour {

    [Header("Настройки движения")]
    public float speed = 10f; // Обычная скорость
    public float sprintMultiplier = 2f; // Ускорение при зажатом Shift
    public float smoothing = 10f; // Сглаживание движения
    private Vector3 _targetVelocity;
    private Vector3 _currentVelocity;

    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        if (Keyboard.current == null) {
            return;
        }

        // Получаем ввод со стрелок
        var inputVector = Vector3.zero;

        if (Keyboard.current.upArrowKey.isPressed) {
            inputVector += transform.forward;
        }

        if (Keyboard.current.downArrowKey.isPressed) {
            inputVector -= transform.forward;
        }

        if (Keyboard.current.leftArrowKey.isPressed) {
            inputVector -= transform.right;
        }

        if (Keyboard.current.rightArrowKey.isPressed) {
            inputVector += transform.right;
        }

        // Проверяем ускорение (Shift)
        var currentSpeed = speed;
        if (Keyboard.current.leftShiftKey.isPressed) {
            currentSpeed *= sprintMultiplier;
        }

        // Вычисляем целевую скорость
        _targetVelocity = inputVector.normalized * currentSpeed;

        // Плавно интерполируем текущую скорость для мягкой остановки и старта
        _currentVelocity = Vector3.Lerp(_currentVelocity, _targetVelocity, Time.deltaTime * smoothing);

        // Применяем движение
        transform.position += _currentVelocity * Time.deltaTime;
    }

}
*/