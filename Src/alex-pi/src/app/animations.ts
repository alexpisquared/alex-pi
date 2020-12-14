import { trigger, transition, style, query, animateChild, group, animate, state, AnimationMetadata, stagger, keyframes } from '@angular/animations';

export const t420e = '620ms ease-in-out';
export const t999eo = '.9s ease-out';
export const t999eo2 = '7s';
export const optnl = { optional: true }; // no visible effect NO: craps in prod (F5 in Contact): - `query(":leave")` returned zero elements. (Use `query(":leave", { optional: true })` if you wish to allow this.)

const r0d = 'rotateX(-00deg)';
const r9d = 'rotateX(+90deg)';

export const slideToFunc = function slideTo(direction: string) {
  return [
    style({ height: 'calc(100vh - 70px)', position: 'relative' }),
    query(
      ':enter, :leave',
      [
        style({
          position: 'absolute',
          top: 0,
          [direction]: 0,
          width: '100%'
          // transform: 'scale(0) translateY(100%)' // see more transitions at https://www.youtube.com/watch?time_continue=173&v=7JA90VI9fAI
        })
      ],
      optnl
    ),
    // query(':enter', [animate(mse, style({ height: 'calc(100vh - 70px)', [direction]: '-100%', transform: 'scale(1) transleteY(0)' }))]),
    // query(':enter', [animate(mse, style({ [direction]: '-100%' }))]),

    query(':enter', [style({ [direction]: '-100%' })]),
    query(':leave', animateChild(), optnl), // no effect
    group([query(':leave', [animate(t420e, style({ [direction]: '100%' }))], optnl), query(':enter', [animate(t420e, style({ [direction]: '0%' }))], optnl)]),
    query(':enter', animateChild()) // no effect
  ];
};

export function slideToFunc_LEFT(): AnimationMetadata[] {
  return [
    style({ height: 'calc(100vh - 70px)', position: 'relative' }),
    query(':enter, :leave', [style({ position: 'absolute', top: 0, left: 0, width: '100%' })], optnl),
    query(':enter', [style({ left: '-100%' })]),
    query(':leave', animateChild(), optnl), // no effect
    group([query(':leave', [animate(t420e, style({ left: '100%' }))], optnl), query(':enter', [animate(t420e, style({ left: '0%' }))])]),
    query(':enter', animateChild(), optnl) // no effect
  ];
}
export function slideToFuncRIGHT(): AnimationMetadata[] {
  return [
    style({ height: 'calc(100vh - 70px)', position: 'relative' }),
    query(':enter, :leave', [style({ position: 'absolute', top: 0, right: 0, width: '100%' })], optnl),
    query(':enter', [style({ right: '-100%' })]),
    query(':leave', animateChild(), optnl), // no effect
    group([query(':leave', [animate(t420e, style({ right: '100%' }))], optnl), query(':enter', [animate(t420e, style({ right: '0%' }))], optnl)]),
    query(':enter', animateChild()) // no effect
  ];
}
export function pivotToFunc_LEFT(): AnimationMetadata[] {
  return [
    style({ height: 'calc(100vh - 70px)', position: 'relative' }),
    query(':enter, :leave', [style({ position: 'absolute', top: 0, left: 0, width: '100%' })], optnl),
    query(':enter', [style({ transform: 'rotate(-90deg)', 'transform-origin': 'top left' })]),
    query(':leave', animateChild(), optnl), // no effect
    group([query(':leave', [animate(t420e, style({ transform: 'rotate(+90deg)', 'transform-origin': 'top left' }))], optnl), query(':enter', [animate(t420e, style({ transform: 'rotate(0deg)', 'transform-origin': 'top left' }))])]),
    query(':enter', animateChild(), optnl) // no effect
  ];
}
export function pivotToFuncRIGHT(): AnimationMetadata[] {
  return [
    style({ height: 'calc(100vh - 70px)', position: 'relative' }),
    query(':enter, :leave', [style({ position: 'absolute', top: 0, right: 0, width: '100%' })], optnl),
    query(':enter', [style({ transform: 'rotate(+90deg)', 'transform-origin': 'top left' })]),
    query(':leave', animateChild(), optnl), // no effect
    group([query(':leave', [animate(t420e, style({ transform: 'rotate(-90deg)', 'transform-origin': 'top left' }))], optnl), query(':enter', [animate(t420e, style({ transform: 'rotate(0deg)', 'transform-origin': 'top left' }))], optnl)]),
    query(':enter', animateChild()) // no effect
  ];
}

export const slideBumpRightToLeftAnimation00 = trigger('slideBumpRightToLeft00', [
  state('isOf', style({ transform: 'scale(0.5) translateY(-100vh)', width: '596px', opacity: 0.25, color: '#666', backgroundColor: '#a9a' })),
  state('isOn', style({ transform: 'scale(1.0) translateY(0%)' })),
  transition('* => isOf', [animate('.1s ease')]),
  transition('* => isOn', [animate('.7s ease')])
  //  transition('* => isOn', [
  //    query('.traffic-light', style({ transform: 'translateX(-40vw)', opacity: 0 })),
  //    query('.traffic-light', stagger('.3s', [animate('.6s', style({ transform: 'translateX(0)', opacity: 1 }))]))]),
  //  transition('* => isOf', [
  //    query('.traffic-light', style({ transform: 'translateX(0)', opacity: 1 })),
  //    query('.traffic-light', stagger('.3s', [animate('.6s', style({ transform: 'translateX(-40vw)', opacity: 0 }))]))])
]);
// export const trafficLightAnimation = trigger('trafficLight', [state('isOn', style({ opacity: 1.0 })), state('isOf', style({ opacity: 0.25 })), transition('* => isOf', [animate('.1s')]), transition('* => isOn', [animate('.2s')])]);
// export const trafficLightAnimation = trigger('trafficLight', [state('isOn', style({ opacity: 0.1 })), state('isOf', style({ opacity: 0.25 })), transition('* => isOf', [animate('.1s')]), transition('* => isOn', [animate('.2s')])]);
// export const trafficLightAnimation = trigger('trafficLight', [
//   transition('* => isOn', [query('div', style({ transform: 'translateX(+40px)', opacity: 1 })), query('div', stagger('.25s', [animate('.5s', style({ transform: 'translateX(+40px)', opacity: 1 }))]))]), // No effect
//   transition('* => isOf', [query('div', style({ transform: 'translateX(-0px)', opacity: 1 })), query('div', stagger('.25s', [animate('.5s', style({ transform: 'translateX(-40px)', opacity: 1 }))]))]) // OK, but self reverses ??!?!?!
// ]);
export const flipinplaceAnimation = trigger('flipinplace', [
  state('isNmb', style({ transform: r9d })),
  state('isDgt', style({ transform: r0d })),
  transition('* => isNmb', animate(t999eo2, keyframes([style({ offset: 0.0, transform: r9d }), style({ offset: 0.98, transform: r9d }), style({ offset: 0.99, transform: r9d + ' skewX(-30deg)' }), style({ offset: 1.0, transform: r0d })]))),
  transition('* => isDgt', animate(t999eo2, keyframes([style({ offset: 0.0, transform: r0d }), style({ offset: 0.98, transform: r0d }), style({ offset: 0.99, transform: r9d + ' skewX(+30deg)' }), style({ offset: 1.0, transform: r9d })])))
]);
export const trafficLightAnimation = trigger('trafficLight', [
  state('isOf', style({ opacity: 0.1 })),
  state('isOn', style({ opacity: 1.0 })),
  transition('* => isOf', animate('2s')),
  transition('* => isOn', animate('2s')) // todo: animatin does not execute -- but just jumos to the end state; could be css style interfering.
]);

export const slideBumpRightToLeftAnimation = trigger('slideBumpRightToLeft', [
  state('isOf', style({ transform: 'translateX(-84vw)' })), // no transition required; it actually makes thisngs wors by flickering FROM the final state => transition('* => isOf', [animate('.1s ease')]),
  state('isOn', style({ transform: 'scale(1.0) translateY(0%)' })),
  transition(
    '* => isOn',
    animate(
      t999eo,
      keyframes([
        style({ offset: 0.0, transform: 'translateX(-84vw) skewX(0)' }), // , color: '#666', backgroundColor: '#a9a' }), //
        style({ offset: 0.75, transform: 'translateX(0) skewX(0)' }), //
        style({ offset: 0.85, transform: 'translateX(+52px) skewX(+20deg)' }), //
        style({ offset: 0.92, transform: 'translateX(-26px) skewX(-10deg)' }), //
        style({ offset: 0.97, transform: 'translateX(+12px) skewX(+05deg)' }), //
        style({ offset: 1.0, transform: 'translateX(0) skewX(0)' })
      ])
    )
  )
]);
export const slideBumpLeftToRightAnimation = trigger('slideBumpLeftToRight', [
  state('isOf', style({ transform: 'translateX(+85vw)' })),
  state('isOn', style({ transform: 'scale(1.0) translateY(0%)' })),
  transition(
    '* => isOn',
    animate(
      t999eo,
      keyframes([
        style({ offset: 0.0, transform: 'translateX(+85vw) skewX(0)' }), //
        style({ offset: 0.75, transform: 'translateX(0) skewX(0)' }), //
        style({ offset: 0.85, transform: 'translateX(-2px) skewX(-20deg)' }), //
        style({ offset: 0.92, transform: 'translateX(+1px) skewX(+10deg)' }), //
        style({ offset: 0.97, transform: 'translateX(-0px) skewX(-5deg)' }), //
        style({ offset: 1.0, transform: 'translateX(0) skewX(0)' })
      ])
    )
  )
]);

export enum VisibilityState {
  Visible = 'visible',
  Hidden = 'hidden'
}

export const stickyHeaderAnimation = trigger('toggle', [
  state(VisibilityState.Hidden, style({ opacity: 0, transform: 'translateY(-100%)' })), //
  state(VisibilityState.Visible, style({ opacity: 1, transform: 'translateY(0)' })),
  transition('* => *', animate('200ms ease-in'))
]);

export const slideInAnimation = trigger('routeAnimations', [
  // transition('HomePage => MyDesignsPage', slideToFunc('right')),   // todo: ERROR during template compile of 'AppComponent' Reference to a non-exported function in contains the error at
  // transition('* => HomePage', slideToFunc('left'))                 // todo: ERROR during template compile of 'AppComponent' Reference to a non-exported function in contains the error at

  transition(
    '* => HomePage, ContactPage => *, MyAppsPage => MyDesignsPage, AboutPage => MyDesignsPage, AboutPage => MyAppsPage, MyDesignsPage => OcrDemoPage, MyAppsPage => OcrDemoPage, AboutPage => OcrDemoPage, ContactPage => OcrDemoPage',
    pivotToFunc_LEFT()
  ),
  transition(
    '* => ContactPage, HomePage => *, MyAppsPage => AboutPage, MyDesignsPage => MyAppsPage, MyDesignsPage => AboutPage, OcrDemoPage => MyDesignsPage, OcrDemoPage => MyAppsPage, OcrDemoPage => AboutPage, OcrDemoPage => ContactPage',
    pivotToFuncRIGHT()
  ) // todo: nogo.
]);
