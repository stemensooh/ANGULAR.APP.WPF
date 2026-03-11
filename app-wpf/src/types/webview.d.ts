interface Window {
  chrome: {
    webview: {
      postMessage: (message: any) => void;
      addEventListener: (type: string, listener: any) => void;
    };
  };
}