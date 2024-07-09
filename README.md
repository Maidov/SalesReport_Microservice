# Sales Report API

API для получения ежемесячных отчетов о продажах

![image](https://github.com/Maidov/SalesReport_Microservice/assets/115298811/36c78b47-9c26-4590-8af4-a606489824ef)

## Описание

Sales Report API предоставляет RESTful интерфейс для получения ежемесячных отчетов о продажах. API позволяет пользователям получать данные о продажах по заданным параметрам.

## Установка и запуск

### Требования

- .NET Core SDK 8 или выше

### Установка

1. Клонируйте репозиторий:
```bash
git clone https://github.com/yourusername/your-repository.git
cd SalesReportAPI
```
2. Запустите PostgeSQL
3. Запустите Zookeeper + Kafka (создайте тему) 
5. Установите зависимости и запустите проект:
```bash
dotnet restore
dotnet build
dotnet run
```
5. API будет доступен по адресу http://localhost:5432

## Использование API
### Регистрация и аутентификация
API поддерживает аутентификацию через JSON Web Token (JWT).

- #### Регистрация нового пользователя
  **Метод:**   ```POST```

  **Путь:**   ```/api/Auth/register```

  **Описание:** Регистрирует нового пользователя в системе.

  **Тело запроса:**
  ```json
  {
  "username": "string",
  "email": "string",
  "password": "string"
  }
  ```
  **Пример запроса:**
  ```bash
  curl -X 'POST' \
    'http://localhost:5120/api/Auth/register' \
    -H 'accept: */*' \
    -H 'Content-Type: application/json' \
    -d '{
    "username": "TestUsername",
    "email": "test@mail.com",
    "password": "TestPassword:12345"
  }'
  ```
  **Пример успешного ответа:**
  ```json
  {
    "message": "User registered successfully"
  }
  ```
- #### Получение JWT токена (аутентификация)
  **Метод:**   ```POST```

  **Путь:**   ```/api/Auth/login```

  **Описание:** Получает JWT токен для аутентификации пользователя.

  **Тело запроса:**
  ```json
  {
  "username": "string",
  "password": "string"
  }
  ```

  **Пример запроса:**
  ```bash
  curl -X 'POST' \
  'http://localhost:5120/api/Auth/login' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "username": "TestUsername",
  "password": "TestPassword:12345"
  }'
  ```

  **Пример успешного ответа:**
  ```json
  {
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlRlc3RVc2VybmFtZSIsIm5iZiI6MTcyMDUzODE2NiwiZXhwIjoxNzIwNTQxNzY2LCJpYXQiOjE3MjA1MzgxNjZ9.GNyCGzTuOaG6zgXbR2v2gPeHV5VJLqY0Ju9CSpor3H4"
  }
  ```

### Эндпоинты
- #### Получение отчетов о продажах
  **Метод:**   ```GET```

  **Путь:**   ```/api/SalesReport/monthly-report```

  **Описание:** Получает отчет о продажах за указанный год и месяц.

  **Параметры пути:**
  ```
  {year} (integer): Год отчета
  
  {month} (integer): Месяц отчета
  
  {category} (string): Категория товаров
  ```
  
  **Пример запроса:**
  ```bash
  curl -X 'GET' \
    'http://localhost:5120/api/SalesReport/monthly-report?year=2023&month=1&category=Drinks' \
    -H 'accept: text/plain'
  ```
  **Пример ответа:**
  ```json
  {
  "category": "Drinks",
  "year": 2023,
  "month": 1,
  "items": [
    {
      "name": "CocaCola",
      "averageRevenue": 165,
      "averageExpenses": 82.3333333333333,
      "averageIncome": 82.6666666666667
    },
    {
      "name": "DrPepper",
      "averageRevenue": 150,
      "averageExpenses": 75,
      "averageIncome": 75.3333333333333
    },
    {
      "name": "SevenUp",
      "averageRevenue": 135,
      "averageExpenses": 67.6666666666667,
      "averageIncome": 67.6666666666667
    }
  ]
  }
  ```

## Ошибки
  API возвращает стандартные HTTP коды ошибок и JSON сообщения с описанием проблемы.

## Тестирование
  Для тестирования API используйте инструменты для отправки HTTP запросов, такие как Postman или curl.

## Совместимость
  API совместимо с любыми клиентскими приложениями, поддерживающими стандартные RESTful API.

## Контакты
   ![1To-sCvVSFw](https://github.com/Maidov/SalesReport_Microservice/assets/115298811/57f5594c-84fe-4f0d-a476-de57472636e1)

  Email: heavenmaido@gmail.com
  
  GitHub: https://github.com/Maidov

