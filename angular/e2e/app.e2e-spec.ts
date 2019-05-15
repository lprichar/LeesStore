import { LeesStoreTemplatePage } from './app.po';

describe('LeesStore App', function() {
  let page: LeesStoreTemplatePage;

  beforeEach(() => {
    page = new LeesStoreTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
