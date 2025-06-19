# 🧾 DailyLogger for Unity

一個輕量、可擴充、具彩色標籤功能的 Unity Log 管理系統，幫助你更清晰地分類並控制各種開發日誌。

---

## 🚀 特點

- 🌈 彩色標籤輸出，依 Logger 類型自動配色
- 🏷 自動附加類別名稱作為 Tag
- 🧩 支援多類型 Logger：開發、系統、框架、遊戲、編輯器
- 🧪 支援 `UnityEngine.Object` Context
- 🔧 可切換是否啟用輸出 / 彩色輸出 / 顯示名稱 Tag
- 🧱 可擴充自訂 Logger 類型
- 🖥 編輯器日誌只在 `Application.isEditor == true` 時輸出

---

## 📦 安裝

1. 使用 Visual Studio 或 Rider 開啟 `DailyLogger.csproj`
2. 編譯成 `.NET Framework 4.x` 的 DLL（與你的 Unity 相容）
3. 將輸出的 `DailyLogger.dll` 放入 Unity 專案中

---

## 🧑‍💻 使用範例

```csharp
// 一般用途
DLogger.Log("這是預設日誌");
DLogger.LogWarning("這是警告");
DLogger.LogError("這是錯誤");

// 分類用 logger
DLogger.DevLog("開發日誌");
DLogger.SysLog("系統日誌");
DLogger.GameLog("遊戲邏輯日誌");
DLogger.EditorLog("只在編輯器中出現的日誌");

// 帶 context 版本
DLogger.GameLog("來自某物件的錯誤", gameObject);

// 進階控制
DLogger.SetEnable<DevLogger>(false);
DLogger.SetColorful<GameLogger>(false);
```

---

## 🧩 自訂 Logger

只要繼承 `BaseLogger`，並使用 `DLogger.Register<你的類型>(實例)` 註冊即可。

```csharp
public class MyLogger : BaseLogger {}

DLogger.Register<MyLogger>(new MyLogger());
DLogger.Log<MyLogger>("這是自訂 logger！");
```

---

## 📁 結構簡介

- `BaseLogger`：基礎日誌格式與行為
- `DLogger`：靜態入口，統一呼叫點
- `DevLogger` / `SysLogger` / `GameLogger` / `FrameWorkLogger` / `EditorLogger`：內建類型

---

## ⚙️ 相容性

- ✅ Unity 2020.3+
- ✅ 支援 WebGL、Editor、Standalone 等平台

---

## 🧾 授權

MIT License

---

## 📮 聯絡與回報

歡迎在 GitHub 上開 issue 或發 PR。如果你覺得這個工具有幫助，別忘了 ⭐ 一下！
