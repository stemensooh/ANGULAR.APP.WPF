import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class HardwareService {
  private pendingRequests = new Map<string, Function>();

  constructor() {
    const webview = window.chrome?.webview;

    if (webview) {
      webview.addEventListener('message', (event: any) => {
        const data = event.data;

        const callback = this.pendingRequests.get(data.requestId);

        if (callback) {
          callback(data);
          this.pendingRequests.delete(data.requestId);
        }
      });
    }
  }

  private send(action: string, data?: any): Promise<any> {
    const requestId = crypto.randomUUID();

    return new Promise((resolve) => {
      this.pendingRequests.set(requestId, resolve);

      window.chrome.webview.postMessage({
        action,
        requestId,
        data,
      });
    });
  }

  async print(ticket: string) {
    return await this.send('printTicket', ticket);
  }

  async getPorts() {
    return await this.send('getPorts');
  }
}
