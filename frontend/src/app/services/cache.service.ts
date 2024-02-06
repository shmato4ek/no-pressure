import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CacheService {
  // A HashMap to store the cache. The key is the page and the value is the data.
  private cache = new Map<string, any[]>();
  // BehaviorSubject that will contain the updated cache data.
  public cache$ = new BehaviorSubject<any[] | undefined>(undefined);

  // The 'set' method for storing data in the cache.
  set(key: string, data: any[]): void {
    // We check if data already exists for this key.
    if (this.cache.has(key)) {
      // If it already exists, we throw an exception to prevent overwriting the data.
    }
    // If there is no data for this key, we store it in the cache and update the BehaviorSubject.
    this.cache.set(key, data);
    this.cache$.next(this.cache.get(key));
  }

  // The 'get' method for retrieving data from the cache.
  get(key: string): any[] | undefined {
    // We retrieve the data from the cache and update the BehaviorSubject.
    const data = this.cache.get(key);
    this.cache$.next(data);
    return data;
  }

  // The 'clear' method to clear data from the cache.
  clear(key: string): void {
    // We remove the data from the cache and update the BehaviorSubject.
    this.cache.delete(key);
    this.cache$.next(undefined);
  }
}