import { APP_BASE_HREF, PlatformLocation } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { APP_INITIALIZER, Injector, NgModule } from "@angular/core";
import { AuthModule, OidcConfigService, OidcSecurityService, StsConfigHttpLoader, StsConfigLoader } from "angular-auth-oidc-client";
import { map, switchMap } from "rxjs";
import { LoginComponent } from "./login/login.component";
import { PostLoginComponent } from "./post-login/post-login.component";

const OIDC_CONFIGURATION = 'assets/auth.clientConfiguration.json';

export const httpLoaderFactory = (httpClient: HttpClient, injector: Injector) => {
  let baseHref = injector.get<string>(APP_BASE_HREF);

  const config$ = httpClient
    .get<any>(`${window.location.origin}${baseHref}${OIDC_CONFIGURATION}`)
    .pipe(
      map((customConfig) => {
        return {
          authority: customConfig.authority,
          redirectUrl: `${window.location.origin}${baseHref}${customConfig.redirect_url}`,
          clientId: customConfig.client_id,
          responseType: customConfig.response_type,
          scope: customConfig.scope,
          postLogoutRedirectUri: `${window.location.origin}${baseHref}${customConfig.post_logout_redirect_uri}`,
          startCheckSession: customConfig.start_checksession,
          silentRenew: customConfig.silent_renew,
          silentRenewUrl: `${window.location.origin}${baseHref}${customConfig.silent_renew_url}`,
          postLoginRoute: customConfig.post_login_route,
          forbiddenRoute: customConfig.forbidden_route,
          unauthorizedRoute: customConfig.unauthorized_route,
          logLevel: customConfig.log_level,
          renewTimeBeforeTokenExpiresInSeconds: customConfig.renewTimeBeforeTokenExpiresInSeconds,
          maxIdTokenIatOffsetAllowedInSeconds: customConfig.max_id_token_iat_offset_allowed_in_seconds,
          historyCleanupOff: customConfig.history_cleanup_off
        };
      })
    );

  return new StsConfigHttpLoader(config$);
};

export function baseHrefFactory(s: PlatformLocation): string {
  return s.getBaseHrefFromDOM();
}

@NgModule({
  declarations: [
    LoginComponent,
    PostLoginComponent
  ],
  imports: [
    AuthModule.forRoot({
      loader: {
        provide: StsConfigLoader,
        useFactory: httpLoaderFactory,
        deps: [HttpClient, Injector]
      },
    })
  ],
  providers: [
    {
      provide: APP_BASE_HREF,
      useFactory: baseHrefFactory,
      deps: [PlatformLocation]
    }
  ],
  exports: [AuthModule],
})
export class AuthHttpConfigModule { }
