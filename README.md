## 📋 Project Information

| Field                  | Description                                                                 |
|-------------------------|-----------------------------------------------------------------------------|
| **Candidate Name**      | Fabio Sarmento Pereira                                                      |
| **Repository**          | [HackerNewsAPI](https://github.com/pereirfa/HackerNewsAPI)                  |
| **Architecture**        | MVC (Controller / Model / Services)                                         |
| **Swagger Documentation** | Not implemented                                                           |
| **Logging/Monitoring**  | Not implemented                                                             |
| **Objective**           | Consume data from the public Hacker News API and expose it via routes       |

---

# HackerNews API

## 📌 Description
This application is an API built with ASP.NET Core that exposes endpoints to query data from the [Hacker News API](https://github.com/HackerNews/API).  
It uses `HttpClient` for external communication and `MemoryCache` to optimize repeated calls.

---

## 🚀 How to Run the Application

### Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) installed
- Internet access (for real calls to the Hacker News API)

### Steps to Run

| #  | Step                        | Command / Action                                                                 |
|----|-----------------------------|----------------------------------------------------------------------------------|
| 1  | Clone the repository        | `git clone https://github.com/pereirfa/HackerNewsAPI.git`                        |
| 2  | Access the project folder   | `cd HackerNewsAPI`                                                                |
| 3  | Restore dependencies        | `dotnet restore`                                                                  |
| 4  | Build the project           | `dotnet build`                                                                    |
| 5  | Run the application         | `dotnet run`                                                                      |
| 6  | Access the API locally      | `http://localhost:5000` or `https://localhost:5001`                               |
| 7  | Test available endpoints    | Example: `GET /Item/{id}` for a specific item, `GET /TopStories` for top stories  |

---

## 📂 Project Structure

---

# 📌 API Routes Summary

## 🔹 ItemController

| Method | Route          | Description                          |
|--------|----------------|--------------------------------------|
| GET    | /v0/item/{id}  | Returns item details by **id**.      |

---

## 🔹 StoriesController

| Method | Route                      | Description                                      |
|--------|----------------------------|--------------------------------------------------|
| GET    | /v0/stories/topstories     | List of most popular stories.                    |
| GET    | /v0/stories/newstories     | List of most recent stories.                     |
| GET    | /v0/stories/beststories    | List of best-rated stories.                      |
| GET    | /v0/stories/askstories     | List of *Ask HN* stories.                        |
| GET    | /v0/stories/showstories    | List of *Show HN* stories.                       |
| GET    | /v0/stories/jobstories     | List of job-related stories.                     |

---

## 🔹 UserController

| Method | Route          | Description                          |
|--------|----------------|--------------------------------------|
| GET    | /v0/user/{id}  | Returns user details by **id**.      |

---

## ⚙️ Cache Functionality in the API

The API uses a service called **CachedHackerNewsService**, which wraps the `HackerNewsService` and adds **in-memory caching** support via ASP.NET Core’s `IMemoryCache`.

### 🔹 Flow Example (with `ItemController`)

| Step | Request Scenario | Behavior                                                                 |
|------|-----------------|--------------------------------------------------------------------------|
| **1. First Request** | `GET /v0/item/{id}` | - Executes `GetItemAsync(id)`.<br>- Checks if the item is in cache.<br>- Since it’s the first call, the item is **not cached** → queries Hacker News via `HttpClient`.<br>- Stores the result in cache for future calls. |
| **2. Second Request (same id)** | `GET /v0/item/{id}` | - Finds the item in cache.<br>- Returns immediately **without calling HttpClient**.<br>- Reduces latency and avoids repeated external calls. |
| **3. Cache Validity** | Any subsequent request | - Cache lifetime is configured via `MemoryCacheOptions`.<br>- While valid, requests for the same `id` are served directly from memory. |

---

### 🔹 Benefits

- **Performance** → Faster responses after the first call.  
- **Efficiency** → Fewer external calls to Hacker News.  
- **Scalability** → Reduces load on external services and improves user experience.  

---

### 🔹 Validation Tests

Unit tests (`CachedHackerNewsServiceTests`) ensure correct cache behavior:

| Test Case | Expected Behavior |
|-----------|------------------|
| First call | `HttpClient` is invoked |
| Second call (same id) | Result comes from cache |
| Validation | Confirms `HttpClient` was called only **once** |


---

### 📌 Next Steps
To make the project more robust and production-ready, the following improvements are suggested:

| Improvement Area | Description |
|------------------|-------------|
| **Swagger/OpenAPI Documentation** | Facilitate testing and route visualization. Generate automatic API documentation. |
| **Structured Logging** | Use libraries like **Serilog** or **NLog**. Record errors, performance metrics, and key events. |
| **Monitoring Configuration** | Integrate tools like **Application Insights**, **Prometheus**, or **Grafana**. Track availability, response times, and API usage. |
| **Error Handling** | Implement standardized error responses. Ensure clear messages for API clients. |
| **Automated Testing** | Create unit and integration tests. Validate behavior of controllers, services, and models. |
| **Additional Documentation** | Explain adopted MVC architecture. Detail data flow between Controller → Service → Model. |

---
