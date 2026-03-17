# Library Checkout API

A tiny API for checking books in and out.

---

## 🚀 Step 1 — Run the app

Open a terminal and run:

```bash
dotnet restore
dotnet run --project src/LibraryCheckout.Api
```

---

## 🌐 Step 2 — Open Swagger

Open this in your browser:

http://localhost:5000

---

## 🧪 Step 3 — Try it (follow these steps exactly)

### 1. Get books

Click:
GET /api/books

Press **Execute**

👉 You will see books and if they are available

---

### 2. Get members

Click:
GET /api/members

Press **Execute**

👉 Copy a `memberId`

---

### 3. Check out a book

Click:
POST /api/checkouts

Click **Try it out**

Paste this (use a real `bookId` + your `memberId`):

```json
{
  "bookId": "10000000-0000-0000-0000-000000000001",
  "memberId": "PASTE_MEMBER_ID_HERE"
}
```

Press **Execute**

---

### 4. See all checkouts

Click:
GET /api/checkouts

👉 You should see your new checkout

---

### 5. Return the book

Copy the `id` from the checkout

Click:
POST /api/checkouts/{checkoutId}/return

Paste the id → Execute

---

## ✅ What this app does

- Shows books  
- Lets you check out a book  
- Lets you return a book  
- Automatically sets a 14-day due date  
- Shows overdue books  

---

## 🧠 Notes

- Data is already pre-loaded (no setup needed)  
- Runs over HTTP for simplicity  
- No login required (easy to test)  

---

## 🔮 If this were real

- Add a database  
- Add tests  
- Add authentication  
- Add interfaces or a strategy approach if the checkout rules started to vary by member type, book type, or branch  


## 🧪 Run tests

```bash
dotnet test
```

👉 Runs a small set of unit tests that validate core business rules.
