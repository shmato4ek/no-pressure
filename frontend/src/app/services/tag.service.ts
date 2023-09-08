import { Injectable } from "@angular/core";
import { ResourceService } from "./resource.service";
import { ActivityDTO } from "../models/activity/activity-dto";
import { HttpClient, HttpErrorResponse, HttpResponse } from "@angular/common/http";
import { Observable, catchError, map, throwError } from "rxjs";
import { Tag } from "../models/tag/tag";
import { TagInfo } from "../models/tag/tag-info";
import { UpdateTag } from "../models/tag/update-tag";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root',
})

export class TagService extends ResourceService<Tag> {
    override getResourceUrl(): string {
        return '/activity';
    }

    constructor (
        override httpClient: HttpClient,
    ) {
        super(httpClient);
    }

    public getAllTagsInfo(id: number)
    {
        return this.getFullRequest<TagInfo[]>(`activity/tag/${id}`)
            .pipe(
                map((resp) => {
                    return resp.body as TagInfo[];
                })
            );
    }

    public updateTag<TRequest, TResponse>(
        resource: TRequest
      ): Observable<HttpResponse<TResponse>> {
        return this.httpClient
          .put<TResponse>(`${environment.apiUrl}/activity/tag`, resource, {observe: 'response' })
      }
}